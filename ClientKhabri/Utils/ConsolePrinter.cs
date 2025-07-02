using Spectre.Console;
using System;

namespace ClientKhabri.Utils
{
    public static class ConsolePrinter
    {
        public static void PrintSuccess(string message)
        {
            SafePrint(() =>
                AnsiConsole.MarkupLine($"[green]✔ {message.EscapeMarkup()}[/]")
            );
        }

        public static void PrintError(string message)
        {
            SafePrint(() =>
                AnsiConsole.MarkupLine($"[red]✖ {message.EscapeMarkup()}[/]")
            );
        }

        public static void PrintWarning(string message)
        {
            SafePrint(() =>
                AnsiConsole.MarkupLine($"[yellow]⚠ {message.EscapeMarkup()}[/]")
            );
        }

        public static void PrintInfo(string message)
        {
            SafePrint(() =>
                AnsiConsole.MarkupLine($"[blue]ℹ {message.EscapeMarkup()}[/]")
            );
        }

        public static void PrintPanel(string title, string message, Color borderColor = default)
        {
            SafePrint(() =>
            {
                if (borderColor == default) borderColor = Color.CornflowerBlue;

                AnsiConsole.Write(
                    new Panel(message.EscapeMarkup())
                        .Header($"[bold]{title.EscapeMarkup()}[/]", Justify.Center)
                        .Border(BoxBorder.Rounded)
                        .BorderStyle(new Style(borderColor))
                        .Padding(1, 1)
                );
            });
        }

        // 🔑 Shared safe print logic
        private static void SafePrint(Action printAction)
        {
            try
            {
                printAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*** AnsiConsole Error");
                Console.WriteLine($"Reason: {ex.Message}");
                Console.WriteLine("Output:");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
