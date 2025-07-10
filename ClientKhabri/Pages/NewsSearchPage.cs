using System;
using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Dtos;
using ClientKhabri.Components;
using static ClientKhabri.Utils.ApiEndpoints;

namespace ClientKhabri.Pages
{
    public static class NewsSearchPage
    {
        public static async Task SearchNewsAsync()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("News Search");
            var term = InputComponent.AskString("Enter search term:");

            NewsSearchResultDto result;
            try
            {
                result = await ServiceProvider.NewsService.SearchNewsAsync(term);
            }
            catch (Exception ex)
            {
                MessageComponent.ShowError($"Failed to fetch news: ");
                MessageComponent.ShowInfo("Press any key to return...");
                Console.ReadKey(true);
                return;
            }

            if (result.Items == null || result.Items.Count == 0)
            {
                MessageComponent.ShowWarning("No news found.");
                MessageComponent.ShowInfo("Press any key to return...");
                Console.ReadKey(true);
                return;
            }

            foreach (var newsItem in result.Items)
            {
                NewsComponent.ShowNews(newsItem);
            }
            MessageComponent.ShowInfo($"\nPage {result.Page} of {Math.Ceiling((double)result.TotalCount / result.PageSize)}");
            MessageComponent.ShowInfo("Press any key to return...");
            Console.ReadKey(true);
        }

    }
}
