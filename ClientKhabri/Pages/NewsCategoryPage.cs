
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Dtos;

namespace ClientKhabri.Pages
{
    public static class NewsCategoryPage
    {
        public static async Task ManageCategoriesAsync()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel("[bold yellow]Manage News Categories[/]")
                    .Header("[bold blue]Category List[/]", Justify.Center)
                    .Border(BoxBorder.Rounded)
                    .BorderStyle(new Style(Color.Orange1))
                    .Padding(2, 1, 2, 1)
            );

            List<CategoryStatusDto> categories;
            try
            {
                categories = await ServiceProvider.CategoryService.GetAllCategoriesAsync();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Failed to fetch categories: {ex.Message}[/]");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            if (categories == null || categories.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No categories found.[/]");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            var choices = new List<string>();
            foreach (var cat in categories)
            {
                var status = cat.IsActive ? "[green]Active[/]" : "[red]Inactive[/]";
                choices.Add($"{cat.CategoryName} (ID: {cat.CategoryID}) {status}");
            }

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a category to toggle active status:")
                    .AddChoices(choices)
            );

            var selectedCat = categories[choices.IndexOf(selected)];

            if (AnsiConsole.Confirm($"Are you sure you want to {(selectedCat.IsActive ? "deactivate" : "activate")} [yellow]{selectedCat.CategoryName}[/]?"))
            {
                try
                {
                    bool result = await ServiceProvider.CategoryService.ToggleActiveAsync(selectedCat.CategoryID);
                    if (result)
                        AnsiConsole.MarkupLine("[green]Category status updated successfully![/]");
                    else
                        AnsiConsole.MarkupLine("[red]Failed to update category status.[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]Error updating category: {ex.Message}[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]No changes made.[/]");
            }

            AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
            Console.ReadKey(true);
        }
    }
}
