using CodingTracker.Controllers;
using CodingTracker.Models;
using Services;
using Spectre.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using CodingTracker.Views;

namespace CodingTracker
{
    public class Program
    {
        private static CodingController codingController;

        public static void Main(string[] args)
        {
            InitializeConfiguration();

            var menu = new Menu(codingController);
            menu.DisplayMenu();

        }


        private static void InitializeConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@".\CodingTracker\appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is not configured properly.");
                return;
            }

            codingController = new CodingController(connectionString);
        }
    }
}