using System;
using System.Threading.Tasks;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public static class AuthDashboard
    {
        public static async Task ShowAsync()
        {
            while (true)
            {
                Console.WriteLine("\n--- Authentication ---");
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ServiceProvider.AuthService.SignUpAsync();
                        break;
                    case "2":
                        await ServiceProvider.AuthService.LoginAsync();
                        await UserDashboard.ShowDashboardAsync();                   
                        break;
                    case "3":
                        Console.WriteLine("Exiting application...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
