using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;

namespace ClientKhabri.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NotificationDto>> GetAllNotificationsAsync()
        {
            var endpoint = ApiEndpoints.Notification.User + Cache.UserID.ToString();
            var response = await ServiceProvider.httpRequestManager.SendAsync<List<NotificationDto>>(HttpMethod.Get, endpoint);
            if (!response.Response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch notifications. Status Code: {response.Response.StatusCode}");

            var jsonResponse = await response.Response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var notifications = JsonSerializer.Deserialize<List<NotificationDto>>(jsonResponse, options);

            return notifications ?? new List<NotificationDto>();
        }
    }
}
