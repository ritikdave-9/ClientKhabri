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
            await AuthPage.ShowAsync();          
        }
    }
}
