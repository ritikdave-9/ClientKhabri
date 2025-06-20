using System;
using System.Threading.Tasks;

namespace ClientKhabri.Services
{
    public class UserDashboard
    {
        private readonly NewsDashboard _newsDashboard;

        public UserDashboard(NewsDashboard newsDashboard)
        {
            _newsDashboard = newsDashboard;
        }

        public async Task ShowDashboardAsync(string userName)
        {
            var now = DateTime.Now;
            var date = now.ToString("dd-MMM-yyyy");
            var time = now.ToString("h:mmtt");

            Console.WriteLine($"\nWelcome to the News Application, {userName}!");
            Console.WriteLine($"Date: {date}");
            Console.WriteLine($"Time: {time}");

            while (true)
            {
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
                        await _newsDashboard.ShowCategoryAsync();
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
}
