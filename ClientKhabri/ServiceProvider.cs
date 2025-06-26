using ClientKhabri.Services.Interface;
using ClientKhabri.Services;
using System.Net.Http;

namespace ClientKhabri
{
    public static class ServiceProvider
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7045/") 
        };

        public static readonly ICategoryService CategoryService = new CategoryService(_httpClient);
        public static readonly INewsService NewsService = new NewsService(_httpClient);
        public static readonly IAuthService AuthService = new AuthService(_httpClient);
    }
}
