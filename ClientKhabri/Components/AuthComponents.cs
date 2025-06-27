using System;
using ClientKhabri.Dtos;
using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class AuthComponents
    {
        public static LoginRequestDto LoginForm()
        {
            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("Email:")
                    .PromptStyle("green"));

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Password:")
                    .PromptStyle("green")
                    .Secret());

            return new LoginRequestDto
            {
                Email = email,
                Password = password
            };
        }

        public static UserSignupDto UserSignupForm()
        {
            var firstName = AnsiConsole.Prompt(
                new TextPrompt<string>("First Name:")
                    .PromptStyle("green"));

            var lastName = AnsiConsole.Prompt(
                new TextPrompt<string>("Last Name:")
                    .PromptStyle("green"));

            var email = AnsiConsole.Prompt(
                new TextPrompt<string>("Email:")
                    .PromptStyle("green"));

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Password:")
                    .PromptStyle("green")
                    .Secret());

            return new UserSignupDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
        }
    }
}
