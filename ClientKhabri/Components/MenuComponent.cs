using Spectre.Console;
using System.Collections.Generic;

namespace ClientKhabri.Components
{
    public static class MenuComponent
    {
        public static string ShowMenu(IEnumerable<string> choices, string title)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(title)
                    .PageSize(15)
                    .AddChoices(choices)
            );
        }
    }
}
