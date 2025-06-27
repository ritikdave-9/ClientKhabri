using System;
using System.Threading.Tasks;
using ClientKhabri.Components;
using ClientKhabri.Services.Interface;
using Spectre.Console;

namespace ClientKhabri.Pages
{
    public static class AuthPage
    {
        public static async Task ShowAsync()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new Panel("[bold yellow]Welcome to Khabri Auth Portal[/]")
                        .Border(BoxBorder.Double)
                        .BorderStyle(new Style(Color.Orange1))
                        .Header("[bold green]Main Menu[/]", Justify.Center)
                        .Padding(2, 1, 2, 1)
                );

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Choose an option:[/]")
                        .PageSize(5)
                        .AddChoices(new[] { "Sign Up", "Login", "Exit" })
                );

                switch (choice)
                {
                    case "Sign Up":
                        var userSignupData = AuthComponents.UserSignupForm();
                        var signupResponse = await ServiceProvider.AuthService.SignUpAsync(userSignupData);
                        if (signupResponse)
                        {
                            AnsiConsole.MarkupLine("[green]Signup successful! Redirecting to dashboard...[/]");
                            await UserPage.ShowUserPageAsync();
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Signup failed. Please try again.[/]");
                        }
                        break;

                    case "Login":
                        var loginRequestData = AuthComponents.LoginForm();
                        var loginResponse = await ServiceProvider.AuthService.LoginAsync(loginRequestData);
                        if (loginResponse)
                        {
                            AnsiConsole.MarkupLine("[green]Login successful! Redirecting to dashboard...[/]");
                            await UserPage.ShowUserPageAsync();
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Login failed. Please try again.[/]");
                        }
                        break;

                    case "Exit":
                        AnsiConsole.MarkupLine("[yellow]Exiting application. Goodbye![/]");
                        Environment.Exit(0);
                        return;
                }

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to menu...[/]");
                Console.ReadKey(true);
            }
        }
    }
}
