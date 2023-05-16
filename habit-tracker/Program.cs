// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.Data.Sqlite;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=habit-tracker.db";
        static void Main(string[]args)
        {
            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS drinking_water (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER
                    )";
                
                tableCmd.ExecuteNonQuery();

                connection.Close();

            }
            GetUserInput();
        }
       
       static void GetUserInput()
       {
        Console.Clear();
        bool closeApp = false;
        while(closeApp == false)
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Type 0 to close app.");
            Console.WriteLine("Type 1 to view all records");
            Console.WriteLine("Type 2 to Insert Record");
            Console.WriteLine("Type 3 to Delete Record");
            Console.WriteLine("Type 4 to Update Record");
            Console.WriteLine("---------------------------");

            string commandInput = Console.ReadLine();

            switch(commandInput){
                case "0":
                    Console.WriteLine("Goodbye");
                    closeApp = true;
                    break;
                case "2":
                    Insert();
                    break;

                    
            }

        }
       }
       
       private static void Insert()
       {
        string date = GetDateInput();

        int quantity = GetNumberInput("Please insert your water intake today?");

        using( var connection = new SqliteConnection(connectionString) )
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"INSERT INTO drinking_water(date,quantity) VALUES('{date}', {quantity})";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
       }
       
       internal static string GetDateInput()
        {
            Console.WriteLine("Please insert the date: (Format: dd-mm-yy). Type 0 to return to the main menu");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") GetUserInput();

            return dateInput;
        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            
            string numberInput = Console.ReadLine();

            if(numberInput == "0") GetUserInput();

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;   
        }
    }
}
