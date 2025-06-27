using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;
using Enums;
using Spectre.Console;

namespace ClientKhabri.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;

        public AuthService(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<bool> SignUpAsync(UserSignupDto signupDto)
        {
            try
            {
                var response = await client.PostAsJsonAsync("/api/User/signup", signupDto);

                if (response.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine("[green]✅ Signup successful![/]");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    var message = response.StatusCode switch
                    {
                        HttpStatusCode.BadRequest => $"[red]❌ Signup failed: Bad Request.[/] [grey]Details: {errorContent}[/]",
                        HttpStatusCode.Conflict => "[red]❌ Signup failed: User already exists.[/]",
                        HttpStatusCode.InternalServerError => "[red]❌ Signup failed: Server error. Please try again later.[/]",
                        _ => $"[red]❌ Signup failed: {response.StatusCode}.[/] [grey]Details: {errorContent}[/]"
                    };

                    AnsiConsole.MarkupLine(message);
                }
            }
            catch (HttpRequestException httpEx)
            {
                AnsiConsole.MarkupLine($"[red]❌ Network or request error:[/] {httpEx.Message}");
            }
            catch (TaskCanceledException)
            {
                AnsiConsole.MarkupLine("[red]❌ Signup request timed out. Please check your network.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ Unexpected error during signup:[/] {ex.Message}");
            }
            return false;
        }

        public async Task<bool> LoginAsync(LoginRequestDto loginDto)
        {
            bool isSuccess = false;

            try
            {
                var response = await client.PostAsJsonAsync("/api/Auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                    var firstName = loginResponse?.FirstName ?? "User";
                    Cache.SetUser(loginResponse?.UserID, loginResponse?.FirstName, loginResponse.Role);
                    AnsiConsole.MarkupLine($"\n[green]✅ Login successful! Welcome, [bold]{firstName}[/].[/]");
                    isSuccess = true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    var message = response.StatusCode switch
                    {
                        HttpStatusCode.Unauthorized => "[red]❌ Login failed: Unauthorized (Invalid username or password).[/]",
                        HttpStatusCode.Forbidden => "[red]❌ Login failed: Forbidden (You do not have access).[/]",
                        HttpStatusCode.BadRequest => $"[red]❌ Login failed: Bad Request.[/] [grey]Details: {errorContent}[/]",
                        HttpStatusCode.InternalServerError => "[red]❌ Login failed: Server error. Please try again later.[/]",
                        _ => $"[red]❌ Login failed: {response.StatusCode}.[/] [grey]Details: {errorContent}[/]"
                    };

                    AnsiConsole.MarkupLine(message);
                }
            }
            catch (HttpRequestException httpEx)
            {
                AnsiConsole.MarkupLine($"[red]❌ Network or request error:[/] {httpEx.Message}");
            }
            catch (TaskCanceledException)
            {
                AnsiConsole.MarkupLine("[red]❌ Login request timed out. Please check your network.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ Unexpected error:[/] {ex.Message}");
            }

            return isSuccess;
        }
    }
}
