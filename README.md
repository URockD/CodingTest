# Hacker News Best Stories API

## Description
This project is a RESTful API built with ASP.NET Core 3.1 to retrieve the top `n` best stories from Hacker News, ranked by their scores. The API efficiently fetches story details using batching and caching mechanisms to handle large numbers of requests without overloading the Hacker News API.

---

## Features
- Fetches the top `n` best stories from Hacker News in descending order of score.
- Supports fallback storage to handle API failures.
- Implements rate limiting and controlled concurrency to optimize performance.
- Utilizes caching for efficient and repeated requests.
- Handles large-scale requests using asynchronous batching.

---

## API Endpoints
### **GET /api/stories?n=2**
Retrieve the top `n` best stories from Hacker News.

#### Request Parameters
- `n` (int): The number of top stories to retrieve.

#### Example Request
```http
GET /api/stories?n=2
```

#### Example Response
```json
[
    {
        "title": "A uBlock Origin update was rejected from the Chrome Web Store",
        "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
        "postedBy": "ismaildonmez",
        "time": "2019-10-12T13:43:01+00:00",
        "score": 1716,
        "commentCount": 572
    },
    {
        "title": "Another Story Title",
        "uri": "https://example.com/story",
        "postedBy": "author",
        "time": "2020-01-01T10:00:00+00:00",
        "score": 1200,
        "commentCount": 300
    }
]
```

---

## Prerequisites
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
- Visual Studio or any other IDE supporting .NET Core development
- A stable internet connection

---

## Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd hacker-news-api
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

---

## Enhancements and Optimizations
1. **Rate Limiting:**
   - Prevents overloading the Hacker News API by limiting concurrent requests using `SemaphoreSlim`.
2. **Asynchronous Batching:**
   - Divides requests into batches for efficient processing.
3. **Caching:**
   - Caches story IDs and details for repeated requests to reduce API load.
4. **Fallback Mechanism:**
   - Saves the latest fetched stories to a local JSON file for use in case of API failure.

---

## Assumptions
- The Hacker News API is available and functional for most requests.
- Story properties like `title`, `url`, and `by` are always present; missing properties are handled gracefully.

---

## Future Enhancements
1. **Pagination Support:**
   - Implement pagination for better handling of large numbers of stories.
2. **Advanced Caching:**
   - Use a distributed caching solution (e.g., Redis) for multi-instance deployments.
3. **Metrics and Monitoring:**
   - Add logging and monitoring to track API performance and usage.
4. **Dockerization:**
   - Provide a Dockerfile to simplify deployment.

---

## License
This project is licensed under the MIT License. See the LICENSE file for details.

