using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class HeaderComponent
    {
        public static void Welcome()
        {
            var panel = new Panel($"[bold yellow]Welcome to the News Application, [green]{Cache.Username}[/]![/]")
                .Header("[bold blue]User Dashboard[/]", Justify.Center)
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Orange1))
                .Padding(2, 1, 2, 1);

            AnsiConsole.Write(new Align(panel, HorizontalAlignment.Center));
        }
        public static void Render(string message)
        {
            var rule = new Rule($"[bold blue]{message}[/]")
            {
                Style = new Style(Color.Yellow), 
                Justification = Justify.Center
            };
            AnsiConsole.Write(rule);
        }
    }
}
