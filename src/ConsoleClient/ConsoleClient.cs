using MealPlanner.Common;
using System;

namespace MealPlanner.Client
{
	/// <summary>
    /// Text-based client to use the application
    /// </summary>
    class ConsoleClient
	{
		static void Main(string[] args)
		{
            DatabaseHandler dbHandler = new DatabaseHandler(GlobalConstants.dbPath);
            Console.Read();
            dbHandler.CreateTables();
            dbHandler.Close();
        }
	}
}
