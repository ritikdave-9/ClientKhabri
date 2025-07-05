using ClientKhabri.Services.Interface;
using ClientKhabri.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.IO;

namespace ClientKhabri
{
    public static class ServiceProvider
    {
        private static readonly IConfiguration _configuration;
        private static readonly HttpClient _httpClient;

        static ServiceProvider()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var baseUrl = _configuration["Api:BaseUrl"];
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            CategoryService = new CategoryService(_httpClient);
            NewsService = new NewsService(_httpClient);
            AuthService = new AuthService(_httpClient);
            httpRequestManager = new HttpRequestManager(_httpClient);
        }

        public static readonly ICategoryService CategoryService;
        public static readonly INewsService NewsService;
        public static readonly IAuthService AuthService;
        public static readonly HttpRequestManager httpRequestManager;
    }
}