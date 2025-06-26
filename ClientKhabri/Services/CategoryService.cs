using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/all"); 

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch categories.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<Category>>(json, options);
        }
    }

}
