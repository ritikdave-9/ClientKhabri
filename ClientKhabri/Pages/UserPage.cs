using System;
using System.Threading.Tasks;
using ClientKhabri.Components;
using ClientKhabri.Services.Interface;
using Spectre.Console;

namespace ClientKhabri.Pages
{
    public static class UserPage
    {
        public async static Task ShowUserPageAsync()
        {
            var now = DateTime.Now;
            var date = now.ToString("dd-MMM-yyyy");
            var time = now.ToString("h:mmtt");

            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel($"[bold yellow]Welcome to the News Application, [green]{Cache.Username}[/]![/]")
                    .Header("[bold blue]User Dashboard[/]", Justify.Center)
                    .Border(BoxBorder.Rounded)
                    .BorderStyle(new Style(Color.Orange1))
                    .Padding(2, 1, 2, 1)
            );

            AnsiConsole.MarkupLine($"[dim]Date:[/] [cyan]{date}[/]   [dim]Time:[/] [cyan]{time}[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[bold]Please choose an option:[/]")
                    .PageSize(5)
                    .AddChoices(new[]
                    {
                        "Headlines",
                        "Saved Articles",
                        "Search (Coming Soon)",
                        "Notifications (Coming Soon)",
                        "Logout"
                    }));

            switch (choice)
            {
                case "Headlines":

                    var categoryResponse = await  ServiceProvider.CategoryService.GetAllCategoriesAsync();
                    var selectedCategory = CategoryComponent.ShowCategory(categoryResponse);
                    var dateRange = DateAndTimeComponent.GetDateRange();
                    break;

                case "Saved Articles":
                    AnsiConsole.MarkupLine("[yellow]Saved Articles feature coming soon![/]");
                    break;

                case "Search (Coming Soon)":
                    AnsiConsole.MarkupLine("[yellow]Search not implemented yet.[/]");
                    break;

                case "Notifications (Coming Soon)":
                    AnsiConsole.MarkupLine("[yellow]Notifications not implemented yet.[/]");
                    break;

                case "Logout":
                    AnsiConsole.MarkupLine("[red]Logging out...[/]");
                    return;
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to dashboard...[/]");
            Console.ReadKey(true);
            await ShowUserPageAsync();
        }
    }
}
