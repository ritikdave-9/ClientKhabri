using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public static class CategoryDashboard
    {

            public static async Task ShowCategoryAsync()
            {
                var categories = await ServiceProvider.CategoryService.GetAllCategoriesAsync();

                if (categories == null || !categories.Any())
                {
                    Console.WriteLine("No categories available.");
                    return;
                }

                Console.WriteLine("Please select a category by number:\n");

                for (int i = 0; i < categories.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {categories[i].CategoryName} ({(categories[i].IsActive ? "Active" : "Inactive")})");
                }

                Console.Write("\nEnter choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= categories.Count)
                {
                    var selectedCategory = categories[choice - 1];
                    Console.WriteLine($"\nYou selected: {selectedCategory.CategoryName} (Slug: {selectedCategory.Slug})");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
         
        }
    }

