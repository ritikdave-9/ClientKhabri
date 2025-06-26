using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<News>> GetNewsByCategoryAsync(string categoryId)
        {
            var response = await _httpClient.GetAsync($"api/news/page?pageNo=1&pageSize=10&categoryId={categoryId}");

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
            var query = $"api/news/save-article?articleId={newsId}&userId={Cache.UserID}";

            var response = await _httpClient.PostAsync(query, null);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to save news: {response.StatusCode}");
                return false;
            }

            Console.WriteLine("News saved successfully.");
            return true;
        }


        private class NewsPageResponse
        {
            public List<News> Items { get; set; }
        }
    }
}
