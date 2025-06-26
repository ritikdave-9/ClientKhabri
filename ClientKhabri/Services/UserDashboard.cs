using System;
using System.Threading.Tasks;
using ClientKhabri.Services.Interface;

namespace ClientKhabri.Services
{
    public static class UserDashboard
    {
        

        public async static Task ShowDashboardAsync()
        {
            var now = DateTime.Now;
            var date = now.ToString("dd-MMM-yyyy");
            var time = now.ToString("h:mmtt");

            Console.WriteLine($"\nWelcome to the News Application, {Cache.Username}!");
            Console.WriteLine($"Date: {date}");
            Console.WriteLine($"Time: {time}");

                Console.WriteLine("\nPlease choose the options below:");
                Console.WriteLine("1. Headlines");
                Console.WriteLine("2. Saved Articles");
                Console.WriteLine("3. Search");
                Console.WriteLine("4. Notifications");
                Console.WriteLine("5. Logout");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Showing headlines...");
                        await NewsDashboard.ShowNewsByCategoryAsync();
                        break;
                    case "2":
                        Console.WriteLine("Showing saved articles...");
                        break;
                    case "3":
                        Console.WriteLine("Search not implemented yet.");
                        break;
                    case "4":
                        Console.WriteLine("Notifications not implemented yet.");
                        break;
                    case "5":
                        Console.WriteLine("Logging out...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            
        }
    }
}
