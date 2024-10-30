using Inventory_Management_System.Repository;
using Inventory_Management_System.Utility;

namespace Inventory_Management_System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n---------------------- Connect To Database ----------------------\n");
        Console.WriteLine("1. SQL Server");
        Console.WriteLine("2. MongoDb");
        Console.WriteLine("3. Exit");
        Console.Write("Please select an option (1-3): ");

        string choice = Console.ReadLine();
        DatabaseType? databaseType = null;
        switch (choice)
        {
            case "1":
                databaseType = DatabaseType.SqlServer;
                break;
            case "2":
                databaseType = DatabaseType.MongoDB;
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Invalid option.");
                return;
        }

        var uI = new ConsoleUI((DatabaseType)databaseType);
        uI.ShowMainMenu();
    }
}
