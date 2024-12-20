using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingTest.Models;
using CodingTest.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            if (n <= 0) return BadRequest("n must be greater than 0");

            var topStories = await _hackerNewsService.GetTopStoriesAsync(n);
            return Ok(topStories);
        }
    }

}
