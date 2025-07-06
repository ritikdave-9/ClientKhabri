using System;
using System.Threading.Tasks;
using ClientKhabri.Components;
using ClientKhabri.Services.Interface;
using Enums;
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
                HeaderComponent.Welcome();
                HeaderComponent.Render("Khabri Auth Portal");
                DateTimeComponent.Render();

                var choice = MenuComponent.ShowMenu(
                    new[] { "Sign Up", "Login", "Exit" },
                    "Choose an option:"
                );

                switch (choice)
                {
                    case "Sign Up":
                        var userSignupData = AuthComponents.UserSignupForm();
                        var signupResponse = await ServiceProvider.AuthService.SignUpAsync(userSignupData);
                        if (signupResponse)
                        {
                            HeaderComponent.Render("Signup successful! Redirecting to dashboard...");
                        }
                        else
                        {
                            HeaderComponent.Render("Signup failed. Please try again.");
                        }
                        break;

                    case "Login":
                        var loginRequestData = AuthComponents.LoginForm();
                        var loginResponse = await ServiceProvider.AuthService.LoginAsync(loginRequestData);
                        if (!loginResponse)
                        {
                            HeaderComponent.Render("Login failed. Please try again.");
                            break;
                        }
                        if (Cache.Role == Role.User)
                        {
                            HeaderComponent.Render("User Login successful! Redirecting to dashboard...");
                            await UserPage.ShowUserPageAsync();
                        }
                        else if (Cache.Role == Role.Admin)
                        {
                            HeaderComponent.Render("Admin Login successful! Redirecting to dashboard...");
                            await AdminPage.ShowAdminPageAsync();
                        }
                        break;

                    case "Exit":
                        HeaderComponent.Render("Exiting application. Goodbye!");
                        Environment.Exit(0);
                        return;
                }

                AnsiConsole.MarkupLine("\n[grey]Press any key to return to menu...[/]");
                Console.ReadKey(true);
            }
        }
    }
}
