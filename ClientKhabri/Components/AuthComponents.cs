using System;
using ClientKhabri.Dtos;
using Spectre.Console;

namespace ClientKhabri.Components
{
    public static class AuthComponents
    {
        public static LoginRequestDto LoginForm()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("Login");
            DateTimeComponent.Render();

            var email = InputComponent.AskString("Email:");
            var password = InputComponent.AskPassword("Password:");

            return new LoginRequestDto
            {
                Email = email,
                Password = password
            };
        }

        public static UserSignupDto UserSignupForm()
        {
            AnsiConsole.Clear();
            HeaderComponent.Render("Sign Up");
            DateTimeComponent.Render();

            var firstName = InputComponent.AskString("First Name:");
            var lastName = InputComponent.AskString("Last Name:");
            var email = InputComponent.AskString("Email:");
            var password = InputComponent.AskPassword("Password:");

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