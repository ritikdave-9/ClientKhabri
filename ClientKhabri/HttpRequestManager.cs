using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<HttpResponseMessage> SendAsync(
            HttpMethod method,
            string path,
            dynamic queryParams = null,
            dynamic body = null,
            dynamic headers = null)
        
        {
            var uriBuilder = new UriBuilder(new Uri(_httpClient.BaseAddress, $"/api{path}"));
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
                var json = JsonSerializer.Serialize(body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {
                var response = await _httpClient.SendAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Unexpected error:[/] {ex.Message}");
                throw;
            }

        }
    }
}
