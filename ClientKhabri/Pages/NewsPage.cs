using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Components; 
namespace ClientKhabri.Pages
{
    public static class NewsPage
    {
        public static async Task ShowAsync()
        {
            var categoryResponse = await ServiceProvider.CategoryService.GetAllCategoriesAsync();

            var selectedCategory = CategoryComponent.ShowCategory(categoryResponse);
            var dateRange = DateAndTimeComponent.GetDateRange();
            var newsResponse = await ServiceProvider.NewsService.GetNewsByCategoryAsync(selectedCategory.CategoryID, dateRange);
            if (newsResponse == null)
            {
                AnsiConsole.MarkupLine("[red]No news found.[/]");
                return;
            }

            foreach (var news in newsResponse)
            {
                NewsComponent.ShowNews(news);
                
            }

            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[bold]What would you like to do?[/]")
                    .AddChoices(new[]
                    {
                        "Back",
                        "Logout",
                        "Save Article",
                        "Report Article"
                    })); ;

            switch (menuChoice)
            {
                case "Back":
                    return;
                case "Logout":
                    AnsiConsole.MarkupLine("[red]Logging out...[/]");
                    Environment.Exit(0);
                    break;
                case "Save Article":
                    bool saveMore;
                    do
                    {
                        var inputNewsId = AnsiConsole.Ask<string>("[bold]Enter the News ID to save:[/]");
                        var saveResult = await ServiceProvider.NewsService.SaveNewsAsync(inputNewsId);

                        saveMore = AnsiConsole.Confirm("Do you want to save another news article?");
                    } while (saveMore);
                    break;
                case "Report Article":
                    bool reportMore;
                    do
                    {
                        var inputNewsId = AnsiConsole.Ask<int>("[bold]Enter the News ID to report:[/]");
                        var reason = AnsiConsole.Ask<string>("[bold]Enter the reason for reporting:[/]");
                        var reportResult = await ServiceProvider.NewsService.ReportNewsAsync(inputNewsId, reason);

                        if (reportResult)
                            AnsiConsole.MarkupLine("[green]News article reported successfully![/]");
                        else
                            AnsiConsole.MarkupLine("[red]Failed to report the news article. Please try again.[/]");

                        reportMore = AnsiConsole.Confirm("Do you want to report another news article?");
                    } while (reportMore);
                    break;
            }
        }
    }
}