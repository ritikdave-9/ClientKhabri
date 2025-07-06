using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class DateTimeComponent
    {
        public static void Render()
        {
            var now = DateTime.Now;
            var date = now.ToString("dd-MMM-yyyy");
            var time = now.ToString("h:mmtt");
            AnsiConsole.MarkupLine($"[dim]Date:[/] [cyan]{date}[/]   [dim]Time:[/] [cyan]{time}[/]");
        }
    }
}
