using System.Net.Http.Json;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;

namespace ClientKhabri.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<News>> GetNewsByCategoryAsync(int categoryId, DateRangeDto dateRange)
        {
            var queryParams = new Dictionary<string, string>
{
             { "startDate", dateRange.From.ToString("yyyy-MM-dd") },
        { "endDate", dateRange.To.ToString("yyyy-MM-dd") },
                { "categoryId", categoryId.ToString() }
};

            var response = await ServiceProvider.httpRequestManager.SendAsync(
                HttpMethod.Get,
                "/News/page",
                queryParams
            );

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to fetch news: {response.StatusCode}");
                return new List<News>();
            }
            var result = await response.Content.ReadFromJsonAsync<NewsPageResponse>();
            return result?.Items ?? new List<News>();
        }
        public async Task<bool> SaveNewsAsync(string newsId)
        {
            var queryParams = new Dictionary<string, string>{
        { "newsId", newsId },
        { "userId", Cache.UserID.ToString() }
            };

            var response = await ServiceProvider.httpRequestManager.SendAsync(
                HttpMethod.Post,
                "/News/save",
                queryParams
            );

            if (!response.IsSuccessStatusCode)
            {
                ConsolePrinter.PrintError($"Failed to save news: {response.StatusCode}");
                return false;
            }

            ConsolePrinter.PrintSuccess("News saved successfully.");
            return true;
        }

        public async Task<List<News>> GetSavedNewsAsync()
        {
            var queryParams = new Dictionary<string, string>
    {
        { "userId", Cache.UserID.ToString() }
    };

            var response = await ServiceProvider.httpRequestManager.SendAsync(
                HttpMethod.Get,
                "/News/saved",
                queryParams
            );

            if (!response.IsSuccessStatusCode)
            {
                ConsolePrinter.PrintError($"Failed to fetch saved news: {response.StatusCode}");
                return new List<News>();
            }

            var result = await response.Content.ReadFromJsonAsync<NewsPageResponse>();
            return result?.Items ?? new List<News>();
        }
        public async Task<bool> DeleteSavedNewsAsync(string newsId)
        {
            var queryParams = new Dictionary<string, string>
    {
        { "newsId", newsId },
        { "userId", Cache.UserID.ToString() }
    };

            var response = await ServiceProvider.httpRequestManager.SendAsync(
                HttpMethod.Delete,
                "/News/saved",
                queryParams
            );

            if (!response.IsSuccessStatusCode)
            {
                ConsolePrinter.PrintError($"Failed to delete news: {response.StatusCode}");
                return false;
            }

            ConsolePrinter.PrintSuccess("News deleted successfully.");
            return true;
        }


        private class NewsPageResponse
        {
            public List<News> Items { get; set; }
        }
    }
}
