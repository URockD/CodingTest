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
            string title = storyData.TryGetProperty("title", out var titleElement) ? titleElement.GetString() : "Untitled";
            string uri = storyData.TryGetProperty("url", out var urlElement) ? urlElement.GetString() : null;
            string postedBy = storyData.TryGetProperty("by", out var byElement) ? byElement.GetString() : "Unknown";
            DateTime time = storyData.TryGetProperty("time", out var timeElement)
                ? DateTimeOffset.FromUnixTimeSeconds(timeElement.GetInt64()).UtcDateTime
                : DateTime.UtcNow;
            int score = storyData.TryGetProperty("score", out var scoreElement) ? scoreElement.GetInt32() : 0;
            int commentCount = storyData.TryGetProperty("descendants", out var descendantsElement) ? descendantsElement.GetInt32() : 0;

            return new Story
            {

                Title = title,
                Uri = uri,
                PostedBy = postedBy,
                Time = time,
                Score = score,
                CommentCount = commentCount
            };
        }
    }
}
