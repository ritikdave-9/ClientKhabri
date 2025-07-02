using System.Net;
using System.Net.Http.Json;
using ClientKhabri.Dtos;
using ClientKhabri.Services.Interface;
using ClientKhabri.Utils;
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
                var response = await ServiceProvider.httpRequestManager.SendAsync(HttpMethod.Post, "/User/signup", body: signupDto);

                if (response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintSuccess("Signup successful!");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    var message = response.StatusCode switch
                    {
                        HttpStatusCode.BadRequest => $"Signup failed: Bad Request. Details: {errorContent}",
                        HttpStatusCode.Conflict => "Signup failed: User already exists.",
                        HttpStatusCode.InternalServerError => "Signup failed: Server error. Please try again later.",
                        _ => $"Signup failed: {response.StatusCode}. Details: {errorContent}"
                    };


                    ConsolePrinter.PrintError(message);
                }
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Unexpected error during signup: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> LoginAsync(LoginRequestDto loginDto)
        {
            bool isSuccess = false;

            try
            {
                var response = await ServiceProvider.httpRequestManager.SendAsync(HttpMethod.Post,"/Auth/login", body:loginDto);
                

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                    var firstName = loginResponse?.FirstName ?? "User";
                    Cache.SetUser(loginResponse.UserID, loginResponse?.FirstName, loginResponse.Role);
                    ConsolePrinter.PrintError($" Login successful! Welcome,{firstName}");
                    isSuccess = true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    var message = response.StatusCode switch
                    {
                        HttpStatusCode.Unauthorized => "Login failed: Unauthorized (Invalid username or password).",
                        HttpStatusCode.Forbidden => "Login failed: Forbidden (You do not have access).",
                        HttpStatusCode.BadRequest => $"Login failed: Bad Request. Details: {errorContent}",
                        HttpStatusCode.InternalServerError => "Login failed: Server error. Please try again later.",
                        _ => $"Login failed: {response.StatusCode}. Details: {errorContent}"
                    };


                    ConsolePrinter.PrintError(message);
                }
            }

            catch (HttpRequestException httpEx)
            {
                ConsolePrinter.PrintError($"Network or request error: {httpEx.Message}");
            }
            catch (TaskCanceledException)
            {
                ConsolePrinter.PrintError("Login request timed out. Please check your network.");
            }
            catch (Exception ex)
            {
                ConsolePrinter.PrintError($"Unexpected error: {ex.Message}");
            }


            return isSuccess;
        }
    }
}
