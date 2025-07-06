using System.Collections.Generic;
using System.Threading.Tasks;
using ClientKhabri.Utils;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public class KeywordService : IKeywordService
    {
        private readonly HttpClient _httpClient;

        public KeywordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KeywordResponseDto> GetKeywordsByCategoryAsync(int categoryId)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "categoryId", categoryId.ToString() }
            };

            var response = await ServiceProvider.httpRequestManager.SendAsync<KeywordResponseDto>(
                HttpMethod.Get,
                ApiEndpoints.Keyword.ByCategory,
                queryParams
            );

            if (response.Response.IsSuccessStatusCode)
            {
                return response.Data ?? new KeywordResponseDto();
            }
            else
            {
                ConsolePrinter.PrintError(response.Error?.Message ?? "Failed to fetch keywords.");
                return new KeywordResponseDto();
            }
        }
    }
}
