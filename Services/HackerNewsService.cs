using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CodingTest.Extensions;
using CodingTest.Factories;
using CodingTest.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CodingTest.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(5);
        private readonly IFallbackStorage _fallbackStorage;


        public HackerNewsService(HttpClient httpClient, IMemoryCache cache, IFallbackStorage fallbackStorage)
        {
            _httpClient = httpClient;
            _cache = cache;
            _fallbackStorage = fallbackStorage;
        }

        public async Task<List<int>> GetBestStoryIdsAsync()
        {
            if (!_cache.TryGetValue("BestStoryIds", out List<int> storyIds))
            {
                var response = await _httpClient.GetStringAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                storyIds = JsonSerializer.Deserialize<List<int>>(response);
                _cache.Set("BestStoryIds", storyIds, TimeSpan.FromMinutes(5));
            }
            return storyIds;
        }

        public async Task<Story> GetStoryDetailsAsync(int id)
        {
            var cacheKey = $"Story_{id}";
            if (_cache.TryGetValue(cacheKey, out Story cachedStory))
            {
                return cachedStory;
            }

            await _semaphore.WaitAsync();
            try
            {
                var json = await _httpClient.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
                var story = StoryFactory.Create(json);
                _cache.Set(cacheKey, story, TimeSpan.FromMinutes(10));
                return story;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<Story>> GetTopStoriesAsync(int n)
        {
            try
            {
                var nBatch = 10;
                var storyIds = await GetBestStoryIdsAsync();
                var batches = storyIds.Take(n).Batch(nBatch);

                var topStories = new List<Story>();

                foreach (var batch in batches)
                {
                    var batchTasks = batch.Select(id => GetStoryDetailsAsync(id));
                    var stories = await Task.WhenAll(batchTasks);
                    topStories.AddRange(stories);
                }

                await _fallbackStorage.SaveAsync(topStories);

                return topStories.OrderByDescending(s => s.Score).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching stories: {ex.Message}. Using fallback data.");
                return await _fallbackStorage.GetFromStorageAsync(n);
            }
        }

    }

}
