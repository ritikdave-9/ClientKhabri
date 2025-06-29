﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/all");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to fetch categories. Status Code: {response.StatusCode}. Response: {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var categories = JsonSerializer.Deserialize<List<string>>(json, options);

                if (categories == null)
                {
                    throw new Exception("Received null when deserializing category list.");
                }

                return categories;
            }
            catch (HttpRequestException ex)
            {
                ConsolePrinter.PrintError($"[HTTP ERROR] {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                ConsolePrinter.PrintError($"[JSON ERROR] Failed to parse category list. {ex.Message}");
                throw new Exception("Invalid response format received from the server.", ex);
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"[GENERAL ERROR] {ex.Message}");
                throw new Exception("An unexpected error occurred while fetching categories.", ex);
            }
        }

    }

}
