using System;
using System.Text.Json;
using CodingTest.Models;

namespace CodingTest.Factories
{
    public static class StoryFactory
    {
        public static Story Create(string json)
        {
            var storyData = JsonSerializer.Deserialize<JsonElement>(json);
            return new Story
            {

                Title = storyData.GetProperty("title").GetString(),
                Uri = storyData.GetProperty("url").GetString(),
                PostedBy = storyData.GetProperty("by").GetString(),
                Time = DateTimeOffset.FromUnixTimeSeconds(storyData.GetProperty("time").GetInt64()).UtcDateTime,
                Score = storyData.GetProperty("score").GetInt32(),
                CommentCount = storyData.GetProperty("descendants").GetInt32()
            };
        }
    }
}
