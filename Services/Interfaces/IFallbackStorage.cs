using System.Collections.Generic;
using System.Threading.Tasks;
using CodingTest.Models;

namespace CodingTest.Services
{
    public interface IFallbackStorage
    {
        Task SaveAsync(List<Story> stories);
        Task<List<Story>> GetFromStorageAsync(int n);
    }
}
