using System.Collections.Generic;
using ClientKhabri.Dtos;
using Spectre.Console;
using ClientKhabri.Components;

namespace ClientKhabri.Components
{
    public static class CategoryComponent
    {
        public static CategoryStatusDto ShowCategory(List<CategoryStatusDto> categories)
        {
            if (categories == null || categories.Count == 0)
            {
                MessageComponent.ShowMessage("No categories available.", "red");
                return null;
            }

            AnsiConsole.Clear();

            HeaderComponent.Render("Please Select a News Category");

            var categoryNames = new List<string>();
            foreach (var category in categories)
            {
                categoryNames.Add(category.CategoryName);
            }

            string selectedCategoryName = MenuComponent.ShowMenu(
                categoryNames,
                "[bold cyan]Choose a category:[/]"
            );

            MessageComponent.ShowMessage($"\nYou selected: [bold]{selectedCategoryName}[/]", "green");

            return categories.Find(c => c.CategoryName == selectedCategoryName);
        }
    }
}