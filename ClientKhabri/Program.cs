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
            await AuthPage.ShowAsync();
            //await NewsPage.ShowAsync();
            

        }
    }
}
