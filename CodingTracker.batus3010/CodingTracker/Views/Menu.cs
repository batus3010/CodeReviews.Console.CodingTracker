
using CodingTracker.Controllers;
using CodingTracker.Models;
using Services;
using Spectre.Console;

namespace CodingTracker.Views
{
    public class Menu
    {
        private readonly CodingController _codingController;

        public Menu(CodingController codingController)
        {
            _codingController = codingController;
        }

        public void DisplayMenu()
        {
            var keepRunning = true;
            while (keepRunning)
            {
                // Display a menu using Spectre.Console
                Display.WelcomeMessage();
                var choice = GetMenuChoice();

                switch (choice)
                {
                    case "Enter new record (CodingSession)":
                        // Call method to enter a new coding session
                        EnterNewCodingSession();
                        break;
                    case "View previous records":
                        // Call method to view previous records
                        ViewPreviousRecords();
                        break;
                    case "Edit a record":
                        // Call method to edit a record
                        EditRecord();
                        break;
                    case "Delete a record":
                        // Call method to delete a record
                        DeleteRecord();
                        break;
                    case "About":
                        Display.AboutMessage();
                        break;
                    case "Exit":
                        ExitProgram();
                        break;
                }
                Display.WaitUserPressKey();
                Console.Clear();
            }
        }
        private string GetMenuChoice()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .PageSize(10)
                    .AddChoices(
                        "View previous records",
                        "Enter new record (CodingSession)",
                        "Edit a record",
                        "Delete a record",
                        "About",
                        "Exit"
                    ));
        }
        private void EnterNewCodingSession()
        {
            var startTime = UserInput.GetDateTimeFromUser("Enter the start time:");
            var endTime = UserInput.GetDateTimeFromUser("Enter the end time:");
            // check if the end time is after the start time
            if (Validation.IsValidTimeRange(startTime, endTime))
            {
                _codingController.AddSession(new CodingSession { StartTime = startTime, EndTime = endTime });
            }
            else
            {
                AnsiConsole.MarkupLine("[red]End time must be after the start time.[/]");
                return;
            }
            AnsiConsole.MarkupLine("[green]Session added successfully.[/]");
        }

        private void ViewPreviousRecords()
        {
            var records = _codingController.GetSessions(); // Retrieve the previous coding sessions from the controller
            if (records.Count == 0)
            {
                Console.WriteLine("No previous records found.");
            }
            else
            {
                var table = new Table();
                table.AddColumn("Record No.");
                table.AddColumn("Start Time");
                table.AddColumn("End Time");
                table.AddColumn("Duration");

                Console.WriteLine("Previous records:");
                foreach (var record in records)
                {
                    table.AddRow(
                        record.Id.ToString(),
                        record.StartTime.ToString(),
                        record.EndTime.ToString(),
                        record.Duration.ToString()
                    );
                }

                AnsiConsole.Write(table);
            }

        }

        private void EditRecord()
        {
            var sessions = _codingController.GetSessions(); // Retrieve the previous coding sessions from the controller
            ViewPreviousRecords();
            int selection;
            while (true)
            {
                Console.Write("Enter the session id: ");
                if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= sessions.Count)
                {
                    break;
                }
                Console.WriteLine("You don't have that session in the database!");
            }

            var selectedSession = sessions[selection - 1];

            Console.WriteLine($"Selected session: Start Time: {selectedSession.StartTime}, End Time: {selectedSession.EndTime}, Duration: {selectedSession.Duration}");

            var newStartTime = UserInput.GetDateTimeFromUser("Enter the new start time:");
            var newEndTime = UserInput.GetDateTimeFromUser("Enter the new end time:");

            if (newEndTime <= newStartTime)
            {
                AnsiConsole.MarkupLine("[red]End time must be after the start time.[/]");
                return;
            }

            selectedSession.StartTime = newStartTime;
            selectedSession.EndTime = newEndTime;

            _codingController.UpdateSession(selectedSession);

            AnsiConsole.MarkupLine("[green]Session updated successfully.[/]");
        }

        private void DeleteRecord()
        {
            // Similar to EditRecord, you would typically prompt the user to select a record by ID, then delete it
            var sessions = _codingController.GetSessions(); // Retrieve the previous coding sessions from the controller
            if (sessions.Count == 0)
            {
                Console.WriteLine("No previous records found.");
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey(true);
                return;
            }
            foreach (var session in sessions)
            {
                Console.WriteLine($"ID: {session.Id}, Start Time: {session.StartTime}, End Time: {session.EndTime}, Duration: {session.Duration}");
            }
            int id;
            while (true)
            {
                Console.Write("Enter the ID of the session to delete: ");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please try again.");
            }
            if(sessions.Exists(s => s.Id == id))
            {
                _codingController.DeleteSession(id);
                AnsiConsole.MarkupLine("[green]Session deleted successfully.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Session not found.[/]");
            }
            //Console.WriteLine("\nPress any key to return to the main menu...");
            //Console.ReadKey(true);
        }

        private static void ExitProgram()
        {
            var exit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Are you sure you want to exit?")
                    .PageSize(10)
                    .AddChoices([
                    "Yes",
                        "No"
                    ]));

            if (exit == "Yes")
            {
                Console.Write("\nPress any key to exit...");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }



    }
}
