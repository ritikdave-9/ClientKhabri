using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClientKhabri.Components;
using ClientKhabri.Pages;

namespace KhabriConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Khabri Console Client");
            //await UserPage.ShowUserPageAsync();
            //await AuthPage.ShowAsync();
            //await UserPage.ShowCategoryAsync();
            DateAndTimeComponent.GetDateRange();

        }
    }
}
