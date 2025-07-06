using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Components;
using Spectre.Console;

namespace ClientKhabri.Pages
{
    public static class ExternalServersPage
    {
        public static async Task ShowAsync()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("External News Servers");
            DateTimeComponent.Render();

            List<NewsSourceDetails> sources;
            try
            {
                sources = await ServiceProvider.NewsSourceService.GetAllNewsSourcesAsync();
            }
            catch (Exception ex)
            {
                HeaderComponent.Render($"Failed to fetch external servers: {ex.Message}");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            if (sources == null || sources.Count == 0)
            {
                HeaderComponent.Render("No external servers found.");
            }
            else
            {
                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .AddColumn("[bold]Name[/]")
                    .AddColumn("[bold]URL[/]")
                    .AddColumn("[bold]Status[/]");

                foreach (var source in sources)
                {
                    table.AddRow(
                        $"[cyan]{source.Name}[/]",
                        $"[blue]{source.BaseURL}[/]",
                        source.Status == Enums.NewsSourceStatus.Active
                            ? "[green]Active[/]"
                            : $"[red]{source.Status}[/]"
                    );
                }

                AnsiConsole.Write(table);
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return to admin menu...[/]");
            Console.ReadKey(true);
        }

        public static async Task EditNewsSourceAsync()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("Edit News Source");
            DateTimeComponent.Render();

            var id = InputComponent.AskString("Enter the ID of the news source to edit:");

            NewsSourceDetails details;
            try
            {
                details = await ServiceProvider.NewsSourceService.GetNewsSourceByIdAsync(id);
            }
            catch (Exception ex)
            {
                HeaderComponent.Render($"Failed to fetch news source: {ex.Message}");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            if (details == null)
            {
                HeaderComponent.Render("News source not found.");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            var updated = NewsSourceComponent.PromptForEdit(details);

            if (InputComponent.Confirm("Save changes?"))
            {
                try
                {
                    bool result = await ServiceProvider.NewsSourceService.UpdateNewsSourceAsync(id, updated);
                    if (result)
                        HeaderComponent.Render("News source updated successfully!");
                    else
                        HeaderComponent.Render("Failed to update news source.");
                }
                catch (Exception ex)
                {
                    HeaderComponent.Render($"Error updating news source: {ex.Message}");
                }
            }
            else
            {
                HeaderComponent.Render("No changes saved.");
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return...");
            Console.ReadKey(true);
        }
    }
}
