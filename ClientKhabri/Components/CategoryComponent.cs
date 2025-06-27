using System;
using System.Collections.Generic;
using ClientKhabri.Dtos;
using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class CategoryComponent
    {
        public static string ShowCategory(List<string> categories)
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

            string selectedCategory = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold cyan]Choose a category:[/]")
                    .PageSize(10)
                    .AddChoices(categories)
            );

            AnsiConsole.MarkupLine($"\n[green]You selected:[/] [bold]{selectedCategory}[/]");
            return selectedCategory;
        }
    }
}
