using Spectre.Console;
using System;
using System.Collections.Generic;

namespace ClientKhabri.Utils
{
    public static class ConsolePrinter
    {
        public static void PrintSuccess(string message)
        {
            AnsiConsole.MarkupLine($"[green]✔ {message}[/]");
        }

        public static void PrintError(string message)
        {
            AnsiConsole.MarkupLine($"[red]✖ {message}[/]");
        }

        public static void PrintWarning(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]⚠ {message}[/]");
        }

        public static void PrintInfo(string message)
        {
            AnsiConsole.MarkupLine($"[blue]ℹ {message}[/]");
        }

        public static void PrintPanel(string title, string message, Color borderColor = default)
        {
            if (borderColor == default) borderColor = Color.CornflowerBlue;

            AnsiConsole.Write(
                new Panel(message)
                    .Header($"[bold]{title}[/]", Justify.Center)
                    .Border(BoxBorder.Rounded)
                    .BorderStyle(new Style(borderColor))
                    .Padding(1, 1)
            );
        }

       

        
    }
}
