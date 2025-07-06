using System;
using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Components;

namespace ClientKhabri.Pages
{
    public static class AdminPage
    {
        public static async Task ShowAdminPageAsync()
        {
            while (true)
            {
                AnsiConsole.Clear();
                HeaderComponent.Render("Admin Dashboard");
                DateTimeComponent.Render();

                var choice = MenuComponent.ShowMenu(
                    new[]
                    {
                        "View External Servers",
                        "Edit Server Details",
                        "Manage News Category",
                        "Logout"
                    }, "Please choose an option:");

                switch (choice)
                {
                    case "View External Servers":
                        await ExternalServersPage.ShowAsync();
                        break;

                    case "Edit Server Details":
                        await ExternalServersPage.EditNewsSourceAsync();
                        break;

                    case "Manage News Category":
                        await NewsCategoryPage.ManageCategoriesAsync();
                        break;

                    case "Logout":
                        Cache.Clear();
                        await AuthPage.ShowAsync();
                        return;
                }

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to admin menu...[/]");
                Console.ReadKey(true);
            }
        }
    }
}
