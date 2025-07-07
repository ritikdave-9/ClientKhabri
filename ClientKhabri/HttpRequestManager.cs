using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using ClientKhabri.Utils;
using Spectre.Console;

namespace ClientKhabri
{
    public class HttpRequestManager
    {
        private readonly HttpClient _httpClient;

        public HttpRequestManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<T>> SendAsync<T>(
            HttpMethod method,
            string path,
            dynamic queryParams = null,
            dynamic body = null,
            dynamic headers = null)
        {
            var uriBuilder = new UriBuilder(new Uri(_httpClient.BaseAddress, $"{path}"));
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var keyValuePair in queryParams)
                {
                    query[keyValuePair.Key] = keyValuePair.Value;
                }
                uriBuilder.Query = query.ToString();
            }

            var request = new HttpRequestMessage(method, uriBuilder.Uri);
            if (!string.IsNullOrWhiteSpace(Cache.AuthToken))
            {
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", $"Bearer {Cache.AuthToken}");
            }

            if (headers != null)
            {
                foreach (var keyValuePair in headers)
                {
                    request.Headers.Remove(keyValuePair.Key);
                    request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            if (body != null && (method == HttpMethod.Post || method == HttpMethod.Put || method.Method == "PATCH"))
            {
                var requestJson = JsonSerializer.Serialize(body);
                request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            }

            try
            {
                var response = await _httpClient.SendAsync(request);

                T data = default;
                MessageResponseDto error = null;
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        try
                        {
                            data = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch(Exception ex)
                        {
                            ConsolePrinter.PrintError($"Exception while mapping the Data of response {ex}");
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        try
                        {
                            error = JsonSerializer.Deserialize<MessageResponseDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                        catch(Exception ex)
                        {
                            ConsolePrinter.PrintError($"Exception while mapping the Data of response {ex}");

                        }
                    }
                }

                return new ApiResponse<T>
                {
                    Response = response,
                    Data = data,
                    Error = error ?? new MessageResponseDto
                    {
                        Message = response.ReasonPhrase,
                    }
                };
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Unexpected error:[/] {ex.Message}");
                throw;
            }
        }
    }
}
