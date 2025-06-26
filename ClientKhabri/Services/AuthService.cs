using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;
using Enums;

namespace ClientKhabri.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService (HttpClient httpClient) {
            client = httpClient;
        
        }
        public async Task SignUpAsync()
        {
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = ConsoleHelper.ReadPassword();

            var signupDto = new UserSignupDto
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            var response = await client.PostAsJsonAsync("/api/User/signup", signupDto);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Signup successful!");
            }
            else
            {
                Console.WriteLine($"Signup failed: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task LoginAsync()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();

            Console.Write("Password: ");
            var password = ConsoleHelper.ReadPassword();

            var loginDto = new LoginRequestDto
            {
                Email = email,
                Password = password
            };

            var response = await client.PostAsJsonAsync("/api/Auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                var firstName = loginResponse?.FirstName ?? "User";
                Cache.SetUser(loginResponse?.UserID,loginResponse?.FirstName,loginResponse.Role);
                Console.WriteLine($"\nLogin successful!");
            }
            else
            {
                Console.WriteLine($"\nLogin failed: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }


    }
}
