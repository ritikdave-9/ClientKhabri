using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;
using ClientKhabri.Dtos;

namespace ClientKhabri.Pages
{
    public static class NotificationPage
    {
        public static async Task ShowNotificationsAsync()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel("[bold yellow]Notifications[/]")
                    .Header("[bold blue]Notification List[/]", Justify.Center)
                    .Border(BoxBorder.Rounded)
                    .BorderStyle(new Style(Color.Orange1))
                    .Padding(2, 1, 2, 1)
            );

            List<NotificationDto> notifications;
            try
            {
                notifications = await ServiceProvider.NotificationService.GetAllNotificationsAsync();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Failed to fetch notifications: {ex.Message}[/]");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            if (notifications == null || notifications.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No notifications found.[/]");
                AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
                Console.ReadKey(true);
                return;
            }

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("ID");
            table.AddColumn("News ID");
            table.AddColumn("Title");
            table.AddColumn("Published At");

            foreach (var n in notifications)
            {
                table.AddRow(
                    n.NotificationId.ToString(),
                    n.NewsId.ToString(),
                    Markup.Escape(n.Title),
                    Markup.Escape(n.PublishedAt.ToString("g"))
                );
            }


            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("\n[grey]Press any key to return...[/]");
            Console.ReadKey(true);
        }
    }
}
