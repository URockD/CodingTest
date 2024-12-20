using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodingTest.Models;

namespace CodingTest.Services
{
    public interface IHackerNewsService
    {
        Task<List<int>> GetBestStoryIdsAsync();
        Task<Story> GetStoryDetailsAsync(int id);
        Task<List<Story>> GetTopStoriesAsync(int n);
    }
}
