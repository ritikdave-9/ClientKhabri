using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;
using ClientKhabri.Dtos;

namespace ClientKhabri.Services
{
    public class NewsSubscribeService : INewsSubscribeService
    {
        private readonly HttpClient _httpClient;

        public NewsSubscribeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SubscribeAsync(int userId, int? categoryId, int? keywordId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };
            if (categoryId.HasValue)
                queryParams.Add("categoryId", categoryId.Value.ToString());
            if (keywordId.HasValue)
                queryParams.Add("keywordId", keywordId.Value.ToString());

            var response = await ServiceProvider.httpRequestManager.SendAsync<MessageResponseDto>(
                HttpMethod.Post,
                ApiEndpoints.NewsSubscribe.Subscribe,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return response.Data?.Message ?? "Subscribed successfully.";
            }
            else
            {
                return response.Error?.Message ?? "Failed to subscribe.";
            }
        }

        public async Task<string> UnsubscribeAsync(int userId, int? categoryId, int? keywordId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "userId", userId.ToString() }
            };
            if (categoryId.HasValue)
                queryParams.Add("categoryId", categoryId.Value.ToString());
            if (keywordId.HasValue)
                queryParams.Add("keywordId", keywordId.Value.ToString());

            var response = await ServiceProvider.httpRequestManager.SendAsync<MessageResponseDto>(
                HttpMethod.Delete,
                ApiEndpoints.NewsSubscribe.Unsubscribe,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return response.Data?.Message ?? "Unsubscribed successfully.";
            }
            else
            {
                return response.Error?.Message ?? "Failed to unsubscribe.";
            }
        }
        public async Task<List<Subscription >> GetUserSubscriptionsAsync(int userId)
        {
            var queryParams = new Dictionary<string, string>
        {
            { "userId", userId.ToString() }
        };

            var response = await ServiceProvider.httpRequestManager.SendAsync<List<Subscription>>(
                HttpMethod.Get,
                ApiEndpoints.NewsSubscribe.GetUserSubscriptions,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return new List<Subscription>();
            }
        }
    }
}
