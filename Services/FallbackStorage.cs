using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CodingTest.Models;

namespace CodingTest.Services
{
    public class FallbackStorage : IFallbackStorage
    {
        private readonly string _fallbackFilePath = "fallback_stories.json";

        public FallbackStorage() { }

        public async Task<List<Story>> GetFromStorageAsync(int n)
        {
            try
            {
                if (!File.Exists(_fallbackFilePath)) return new List<Story>();

                var jsonData = await File.ReadAllTextAsync(_fallbackFilePath);
                var stories = JsonSerializer.Deserialize<List<Story>>(jsonData);

                return stories?.Take(n).OrderByDescending(s => s.Score).ToList() ?? new List<Story>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read fallback storage: {ex.Message}");
                return new List<Story>();
            }
        }

        public async Task SaveAsync(List<Story> stories)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(stories);
                await File.WriteAllTextAsync(_fallbackFilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save fallback storage: {ex.Message}");
            }
        }
    }
}
