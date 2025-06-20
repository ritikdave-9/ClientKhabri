using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientKhabri.Services;

namespace KhabriConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Khabri Console Client");

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7045/");
           

            var categoryService = new CategoryService(client);
            var newsDashboard = new NewsDashboard(categoryService);
            var userDashboard = new UserDashboard(newsDashboard);

            //await userDashboard.ShowDashboardAsync("Suresh");


            while (true)
            {
                Console.WriteLine("\n1. Sign Up");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AuthService.SignUpAsync(client);
                        break;
                    case "2":
                        await AuthService.LoginAsync(client);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
