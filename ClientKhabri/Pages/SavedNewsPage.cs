using System.Collections.Generic;
using System.Threading.Tasks;
using ClientKhabri.Components;
using Spectre.Console;

namespace ClientKhabri.Pages
{
    public static class SavedNewsPage
    {
        public static async Task ShowAsync()
        {

            var savedNews = await ServiceProvider.NewsService.GetSavedNewsAsync();

            if (savedNews == null || savedNews.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No saved news articles found.[/]");
                return;
            }

            foreach (var news in savedNews)
            {
                NewsComponent.ShowNews(news);
            }
           
                var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n[bold]What would you like to do?[/]")
                        .AddChoices(new[]
                        {
                            "Go to Main Menu",
                            "Delete Saved News"
                        }));

                switch (menuChoice)
                {
                    case "Go to Main Menu":
                        await UserPage.ShowUserPageAsync();
                        return;
                    case "Delete Saved News":
                        var newsIdToDelete = AnsiConsole.Ask<string>("[bold]Enter the News ID to delete:[/]");
                        var result = await ServiceProvider.NewsService.DeleteSavedNewsAsync(newsIdToDelete);
                        break;
                }
            }
            
        }
    }
