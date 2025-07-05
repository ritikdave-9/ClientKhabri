using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class NewsComponent
    {
        public static void ShowNews(dynamic news)
        {
            var title = Markup.Escape(news.Title ?? "No Title");
            var description = Markup.Escape(news.Description ?? "No Description");
            var url = Markup.Escape(news.Url ?? "N/A");
            var newsId = Markup.Escape(news.NewsID?.ToString() ?? "N/A");
            var publishedAt = news.PublishedAt != null
                ? ((DateTime)news.PublishedAt).ToString("yyyy-MM-dd HH:mm")
                : "N/A";

            AnsiConsole.Write(
                new Panel(
                    $"[grey]Id:[/] [cyan]{newsId}[/]\n" +
                    $"[grey]Title:[/] [bold yellow]{title}[/]\n\n" +
                    $"[grey]Description:[/] [white]{description}[/]\n\n" +
                    $"[grey]Url:[/] [cyan]{url}[/]\n" +
                    $"[grey]Published At:[/] [cyan]{publishedAt}[/]"
                )
                .Header("[bold blue]News Article[/]", Justify.Left)
                .Border(BoxBorder.Rounded)
                .Padding(1, 1, 1, 1)
            );
            int width = Math.Max(Console.WindowWidth - 1, 1);
            AnsiConsole.MarkupLine($"[dim]{new string('-', width)}[/]");
        }
        
    }
}
