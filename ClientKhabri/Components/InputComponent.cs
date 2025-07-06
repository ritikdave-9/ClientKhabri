using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class InputComponent
    {
        public static string AskString(string prompt)
        {
            return AnsiConsole.Ask<string>(prompt);
        }

        public static int AskInt(string prompt)
        {
            return AnsiConsole.Ask<int>(prompt);
        }

        public static bool Confirm(string prompt)
        {
            return AnsiConsole.Confirm(prompt);
        }
        public static string AskPassword(string prompt)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                    .PromptStyle("green")
                    .Secret());
        }
    }
}
