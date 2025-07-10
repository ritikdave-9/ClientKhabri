using System.Net.Http.Json;
using System.Text.Json;
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
            try
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "startDate", dateRange.From.ToString("yyyy-MM-dd") },
                    { "endDate", dateRange.To.ToString("yyyy-MM-dd") },
                    { "categoryId", categoryId.ToString() }
                };

                var response = await ServiceProvider.httpRequestManager.SendAsync<NewsPageResponse>(
                    HttpMethod.Get,
                    ApiEndpoints.News.Page,
                    queryParams
                );

                if (!response.Response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintError($"Failed to fetch news: {response.Response.StatusCode}");
                    return new List<News>();
                }
                return response?.Data.Items ?? new List<News>();
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error fetching news: ");
                return new List<News>();
            }
        }

        public async Task<bool> SaveNewsAsync(string newsId)
        {
            try
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "newsId", newsId },
                    { "userId", Cache.UserID.ToString() }
                };

                var response = await ServiceProvider.httpRequestManager.SendAsync<dynamic>(
                    HttpMethod.Post,
                    ApiEndpoints.News.Save,
                    queryParams
                );

                if (!response.Response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintError($"Failed to save news: {response.Response.StatusCode}");
                    return false;
                }

                ConsolePrinter.PrintSuccess("News saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error saving news: ");
                return false;
            }
        }

        public async Task<List<News>> GetSavedNewsAsync()
        {
            try
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "userId", Cache.UserID.ToString() }
                };

                var response = await ServiceProvider.httpRequestManager.SendAsync<NewsPageResponse>(
                    HttpMethod.Get,
                    ApiEndpoints.News.GetAllSaved,
                    queryParams
                );

                if (!response.Response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintError($"Failed to fetch saved news: {response.Response.StatusCode}");
                    return new List<News>();
                }

                return response.Data?.Items ?? new List<News>();
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error fetching saved news: ");
                return new List<News>();
            }
        }

        public async Task<bool> DeleteSavedNewsAsync(string newsId)
        {
            try
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "newsId", newsId },
                    { "userId", Cache.UserID.ToString() }
                };

                var response = await ServiceProvider.httpRequestManager.SendAsync<dynamic>(
                    HttpMethod.Delete,
                    ApiEndpoints.News.DeleteSaaved,
                    queryParams
                );

                if (!response.Response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintError($"Failed to delete news: {response.Error.Message}");
                    return false;
                }

                ConsolePrinter.PrintSuccess("News deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error deleting news: ");
                return false;
            }
        }

        public async Task<bool> ReportNewsAsync(int newsId, string reason)
        {
            try
            {
                var payload = new {reason};
                var query = new Dictionary<string, string>
{
    { "userId", Cache.UserID.ToString() },
    { "newsId", newsId.ToString() }
};
                var response = await ServiceProvider.httpRequestManager.SendAsync<dynamic>(
                    HttpMethod.Post,
                    ApiEndpoints.Report.News,
                    query,
                    body: payload
                );

                if (!response.Response.IsSuccessStatusCode)
                {

                    ConsolePrinter.PrintError($"Failed to report news: {response.Response.StatusCode} ,{response.Error.Message}");
                    return false;
                }
                ConsolePrinter.PrintSuccess("News reported successfully.");
                return true;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error reporting news: ");
                return false;
            }
        }
        public async Task<NewsSearchResultDto> SearchNewsAsync(string term, int pageNo = 1, int pageSize = 10)
        {
            var query = $"?term={Uri.EscapeDataString(term)}&pageNo={pageNo}&pageSize={pageSize}";
            var response = await ServiceProvider.httpRequestManager.SendAsync<NewsSearchResultDto>(
                HttpMethod.Get, ApiEndpoints.News.Search + query);

            if (!response.Response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch news. Status Code: {response.Response.StatusCode}");

            var jsonResponse = await response.Response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<NewsSearchResultDto>(jsonResponse, options);

            return result ?? new NewsSearchResultDto();
        }

        public async Task<PersonalizedNewsResponse> GetPersonalizedNewsAsync(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                var query = new Dictionary<string, string>
{
    { "userId", Cache.UserID.ToString() },
    { "pageNo", pageNo.ToString() },
    { "pageSize", pageSize.ToString() }
};
                var response = await ServiceProvider.httpRequestManager.SendAsync<PersonalizedNewsResponse>(
                    HttpMethod.Get,
                    ApiEndpoints.News.Personalized,
                    queryParams:query
                );

                if (!response.Response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to fetch personalized news. Status Code: {response.Response.StatusCode}. message: {response.Error?.Message}");
                }

                var news = response.Data;

                if (news == null)
                {
                    throw new Exception("Received null when deserializing personalized news list.");
                }

                return news;
            }
            catch (HttpRequestException ex)
            {
                ConsolePrinter.PrintError($"[HTTP ERROR] ");
                throw;
            }
            catch (JsonException ex)
            {
                ConsolePrinter.PrintError($"[JSON ERROR] Failed to parse personalized news list. ");
                throw new Exception("Invalid response format received from the server.", ex);
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"[GENERAL ERROR] ");
                throw new Exception("An unexpected error occurred while fetching personalized news.", ex);
            }
        }
        private class NewsPageResponse
        {
            public List<News> Items { get; set; }
        }
        
    }
}