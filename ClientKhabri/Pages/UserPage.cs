using Spectre.Console;
using ClientKhabri.Components;

namespace ClientKhabri.Pages
{
    public static class UserPage
    {
        public async static Task ShowUserPageAsync()
        {
            AnsiConsole.Clear();

            HeaderComponent.Welcome();
            DateTimeComponent.Render();


            var choice = MenuComponent.ShowMenu(
                new[]
                {
                    "Headlines",
                    "Personalized News",
                    "Saved Articles",
                    "Search News",
                    "Notifications",
                    "Subscribe",
                    "Unsubscribe",
                    "Logout"
                },
                "\n[bold]Please choose an option:[/]"
            );

            switch (choice)
            {
                case "Headlines":
                    await NewsPage.ShowAsync();
                    break;
                case "Personalized News":
                    await NewsPage.ShowPersonalizedAsync();
                    break;
                case "Saved Articles":
                    await SavedNewsPage.ShowAsync();
                    break;
                case "Notifications":
                    await NotificationPage.ShowNotificationsAsync();
                    break;
                case "Search News":
                    await NewsSearchPage.SearchNewsAsync();
                    break;
                case "Subscribe":
                    await SubscribePage.ShowSubscribeAsync();
                    break;
                case "Unsubscribe":
                    await SubscribePage.ShowUnsubscribeAsync();
                    break;
                case "Logout":
                    Cache.Clear();
                    await AuthPage.ShowAsync();
                    return;
            }

            MessageComponent.ShowMessage("Press any key to return to dashboard...");
            Console.ReadKey(true);
            await ShowUserPageAsync();
        }
    }
}
