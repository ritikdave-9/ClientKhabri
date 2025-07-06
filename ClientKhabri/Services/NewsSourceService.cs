using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;

namespace ClientKhabri.Services
{
    public class NewsSourceService : INewsSourceService
    {
        private readonly HttpClient _httpClient;

        public NewsSourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       public async Task<List<NewsSourceDetails>> GetAllNewsSourcesAsync()
        {
            try
            {
                var response = await ServiceProvider.httpRequestManager.SendAsync<List<NewsSourceDetails>>(
                    HttpMethod.Get,
                    ApiEndpoints.NewsSource.GetAll,
                    null
                );

                if (response.Response.IsSuccessStatusCode && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    ConsolePrinter.PrintError($"Failed to fetch news sources: {response.Response.StatusCode}");
                    return new List<NewsSourceDetails>();
                }
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error fetching news sources: {ex.Message}");
                return new List<NewsSourceDetails>();
            }
        }
        public async Task<bool> UpdateNewsSourceAsync(string id, NewsSourceDetails updated)
        {
            try
            {
                var response = await ServiceProvider.httpRequestManager.SendAsync<dynamic>(
                    HttpMethod.Put,
                    ApiEndpoints.NewsSource.Edit + id,
                    null,
                    body: updated
                );

                return response.Response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                {
                    ConsolePrinter.PrintError($"[ERROR] Failed to update news source: {ex.Message}");
                    throw ex;
                }
            }
        }

        public async Task<NewsSourceDetails> GetNewsSourceByIdAsync(string id)
        {
            try
            {
                var response = await ServiceProvider.httpRequestManager.SendAsync<NewsSourceDetails>(
                    HttpMethod.Get,
                    ApiEndpoints.NewsSource.GetById + id,
                    null
                );

                if (response.Response.IsSuccessStatusCode && response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    ConsolePrinter.PrintError($"Failed to fetch news source: {response.Response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Error fetching news source: {ex.Message}");
                return null;
            }
        }

    }
}
  