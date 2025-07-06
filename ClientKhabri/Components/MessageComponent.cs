using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class MessageComponent
    {
        public static void ShowMessage(string message, string color = "grey")
        {
            AnsiConsole.MarkupLine($"\n[{color}]{message}[/]");
        }
        public static void ShowWarning(string message)
        {
            AnsiConsole.MarkupLine($"[yellow]{message}[/]");
        }

        public static void ShowError(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }

        public static void ShowInfo(string message)
        {
            AnsiConsole.MarkupLine($"[grey]{message}[/]");
        }
    }
}
