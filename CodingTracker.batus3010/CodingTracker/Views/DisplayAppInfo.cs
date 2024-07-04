

using Spectre.Console;

namespace Services
{
    public static class DisplayAppInfo
    {
        public static void WelcomeMessage() =>
            Console.WriteLine(@"
 ██████╗ ██████╗ ██████╗ ██╗███╗   ██╗ ██████╗     ████████╗██████╗  █████╗  ██████╗██╗  ██╗███████╗██████╗ 
██╔════╝██╔═══██╗██╔══██╗██║████╗  ██║██╔════╝     ╚══██╔══╝██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗
██║     ██║   ██║██║  ██║██║██╔██╗ ██║██║  ███╗       ██║   ██████╔╝███████║██║     █████╔╝ █████╗  ██████╔╝
██║     ██║   ██║██║  ██║██║██║╚██╗██║██║   ██║       ██║   ██╔══██╗██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗
╚██████╗╚██████╔╝██████╔╝██║██║ ╚████║╚██████╔╝       ██║   ██║  ██║██║  ██║╚██████╗██║  ██╗███████╗██║  ██║
 ╚═════╝ ╚═════╝ ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝        ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝                                                                                                
Welcome to the Coding Tracker
Use the Up and Down arrow key to cycle through options, press Enter to select
");

        public static void AboutMessage()
        {
            AnsiConsole.WriteLine("====================");
            AnsiConsole.WriteLine("About Coding Tracker");
            AnsiConsole.WriteLine("====================");
            AnsiConsole.WriteLine("Version: 1.0.0");
            AnsiConsole.WriteLine("Author: batus");
            AnsiConsole.WriteLine("Description: This application helps you track your coding sessions and manage your projects efficiently.");
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine("Press any key to return to the main menu...");
            System.Console.ReadKey(true);
            System.Console.Clear();
        }
    }
}
