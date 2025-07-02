using System;
using System.Collections.Generic;
using ClientKhabri.Dtos;
using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class CategoryComponent
    {
        public static CategoryDto ShowCategory(List<CategoryDto> categories)
        {
            if (categories == null || categories.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No categories available.[/]");
                return null;
            }

            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel("[bold yellow]Please Select a News Category[/]")
                    .Border(BoxBorder.Double)
                    .Header("[green]Categories[/]", Justify.Center)
                    .BorderStyle(new Style(Color.Orange1))
            );

            var categoryNames = new List<string>();
            foreach (var category in categories)
            {
                categoryNames.Add(category.CategoryName);
            }

            string selectedCategoryName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Choose a category:[/]")
                    .PageSize(10)
                    .AddChoices(categoryNames)
            );

            AnsiConsole.MarkupLine($"\n[green]You selected:[/] [bold]{selectedCategoryName}[/]");

            return categories.Find(c => c.CategoryName == selectedCategoryName);
        }

    }
}
