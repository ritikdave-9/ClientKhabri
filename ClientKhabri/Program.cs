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
            //await UserDashboard.ShowDashboardAsync();
            await AuthDashboard.ShowAsync();


        }
    }
}
