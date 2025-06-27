//using ClientKhabri.Services.Interface;
//using System;

//namespace ClientKhabri.Pages
//{
//    public static class NewsPage
//    {
//        public async static Task ShowNewsByCategoryAsync()
//        {
//            var categories = await ServiceProvider.CategoryService.GetAllCategoriesAsync();

//            if (categories == null || !categories.Any())
//            {
//                Console.WriteLine("No categories available.");
//                return;
//            }

//            Console.WriteLine("Select a category:\n");
//            for (int i = 0; i < categories.Count; i++)
//            {
//                Console.WriteLine($"{i + 1}. {categories[i].CategoryName} ({(categories[i].IsActive ? "Active" : "Inactive")})");
//            }

//            Console.Write("\nEnter choice: ");
//            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= categories.Count)
//            {
//                var selectedCategory = categories[choice - 1];

//                Console.WriteLine($"\nFetching news for: {selectedCategory.CategoryName}...\n");
//                var newsList = await ServiceProvider.NewsService.GetNewsByCategoryAsync(Convert.ToString(selectedCategory.CategoryID));

//                if (!newsList.Any())
//                {
//                    Console.WriteLine("No news found in this category.");
//                    return;
//                }

//                Console.Clear();
//                Console.WriteLine($"Welcome to the News Application, Suresh! Date: {DateTime.Now:dd-MMM-yyyy} Time: {DateTime.Now:h:mmtt}\n");
//                Console.WriteLine("H E A D L I N E S");


//                foreach (var news in newsList)
//                {
//                    Console.WriteLine($"Article Id: {news.NewsID}");
//                    Console.WriteLine($"{news.Title}");
//                    Console.WriteLine($"{(news.Description?.Length > 200 ? news.Description.Substring(0, 200) + "…" : news.Description)}");
//                    Console.WriteLine($"source: {news.Source}");
//                    Console.WriteLine("URL:");
//                    Console.WriteLine(news.Url);
//                    Console.WriteLine($"{selectedCategory.CategoryName}: {selectedCategory.CategoryName.ToLower()}\n");
//                }
//                Console.WriteLine("1. Back");
//                Console.WriteLine("2. Logout");
//                Console.WriteLine("3. Save Article\n");

//                string userInput = Console.ReadLine();

//                switch (userInput)
//                {
//                    case "1":
//                        await ShowNewsByCategoryAsync();
//                        break;
//                    case "2":
//                        await AuthPage.ShowAsync();
//                        break;
//                    case "3":
//                        Console.WriteLine("Enter Article Id to Save : ");
//                        string articleId = Console.ReadLine();
//                        await ServiceProvider.NewsService.SaveNewsAsync(articleId);
//                        break;

//                    default:
//                        Console.WriteLine("Invalid choice. Please try again.");
//                        break;
//                }

//            }
//            else
//            {
//                Console.WriteLine("Invalid choice. Try again.");
//            }
//        }
//    }
//}
