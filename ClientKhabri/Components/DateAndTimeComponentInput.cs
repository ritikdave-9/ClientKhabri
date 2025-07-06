using ClientKhabri.Dtos;
using Spectre.Console;
using System;

namespace ClientKhabri.Components
{
    
    public class DateAndTimeComponentInput
    {
        public static DateRangeDto GetDateRange()
        {
            HeaderComponent.Render("Select the Date Range");
            var choises = new List<string>() {"Today",
                        "Yesterday",
                        "Last 7 Days",
                        "Last 30 Days",
                        "Custom"
            };

            var choice = MenuComponent.ShowMenu(
                choises,
                "[bold cyan]Select a date range:[/]"
            );

            DateTime from, to;

            switch (choice)
            {
                case "Today":
                    from = to = DateTime.Today;
                    break;
                case "Yesterday":
                    from = to = DateTime.Today.AddDays(-1);
                    break;
                case "Last 7 Days":
                    to = DateTime.Today;
                    from = to.AddDays(-6);
                    break;
                case "Last 30 Days":
                    to = DateTime.Today;
                    from = to.AddDays(-29);
                    break;
                default:
                    from = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>("Enter [green]From Date[/] (yyyy-MM-dd):")
                            .Validate(date =>
                                date <= DateTime.Today
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]Date cannot be in the future.[/]")));

                    to = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>("Enter [green]To Date[/] (yyyy-MM-dd):")
                            .Validate(date =>
                                date <= DateTime.Today.AddDays(1) && date >= from
                                    ? ValidationResult.Success()
                                    : ValidationResult.Error("[red]To date must be after or equal to From date and not in future.[/]")));
                    break;
            }

            AnsiConsole.MarkupLine($"\n[bold green]✔ Selected Date Range:[/] [cyan]{from:yyyy-MM-dd}[/] to [cyan]{to:yyyy-MM-dd}[/]\n");
            AnsiConsole.Clear();
            return new DateRangeDto
            {
                From = from,
                To = to
            };
        }
    }
}
