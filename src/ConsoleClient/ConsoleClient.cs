using System;
using System.Data.SQLite;

namespace MealPlanner
{
	class ConsoleClient
	{
        const string path_to_db = "C:\\sqlite\\MealPlanner\\MealPlanner.sqlite";
		static void Main(string[] args)
		{
            DatabaseHandler h = new DatabaseHandler(path_to_db);
            h.CreateTables();
            h.Close();
        }
	}
}
