using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;

namespace ClientKhabri.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryStatusDto>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await ServiceProvider.httpRequestManager.SendAsync<List<CategoryStatusDto>>(HttpMethod.Get, path: ApiEndpoints.Category.GetAll);

                if (!response.Response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to fetch categories. Status Code: {response.Response.StatusCode}");
                }

               

                var categories = response.Data ;

                if (categories == null)
                {
                    throw new Exception("Received null when deserializing category list.");
                }

                return categories;
            }
            catch (HttpRequestException ex)
            {
                ConsolePrinter.PrintError($"[HTTP ERROR] ");
                throw;
            }
            catch (JsonException ex)
            {
                ConsolePrinter.PrintError($"[JSON ERROR] Failed to parse category list. ");
                throw new Exception("Invalid response format received from the server.", ex);
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"[GENERAL ERROR] ");
                throw new Exception("An unexpected error occurred while fetching categories.", ex);
            }
        }
        public async Task<bool> ToggleActiveAsync(int id)
        {
            try
            {
                var endpoint = ApiEndpoints.Category.ToggleActive + id.ToString();
                var response = await ServiceProvider.httpRequestManager.SendAsync<CategoryDto>(HttpMethod.Put, endpoint);
                return response.Response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"[ERROR] Failed to toggle category active state: ");
                return false;
            }
        }

    }
}
