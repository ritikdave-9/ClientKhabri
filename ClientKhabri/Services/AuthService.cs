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
                var response = await ServiceProvider.httpRequestManager.SendAsync<dynamic>(HttpMethod.Post, ApiEndpoints.User.Signup, body: signupDto);

                if (response.Response.IsSuccessStatusCode)
                {
                    ConsolePrinter.PrintSuccess("Signup successful!");
                    return true;
                }
                else
                {
                    ConsolePrinter.PrintError(response.Error.Message);
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
                var response = await ServiceProvider.httpRequestManager.SendAsync<LoginResponseDto>(HttpMethod.Post,ApiEndpoints.Auth.Login, body:loginDto);
                

                if (response.Response.IsSuccessStatusCode)
                {
                    var firstName = response.Data?.FirstName ?? "User";
                    Cache.SetUser(response.Data.UserID, response.Data?.FirstName, response.Data.Role);
                    ConsolePrinter.PrintError($" Login successful! Welcome,{firstName}");
                    isSuccess = true;
                }
                else
                {

                    ConsolePrinter.PrintError(response.Error.Message);
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
