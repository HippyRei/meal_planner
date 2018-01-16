using System;
using System.Data.SQLite;

namespace MealPlanner
{
	class ConsoleClient
	{
		static void Main(string[] args)
		{
            Console.WriteLine("Connecting to MealPlanner.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=MealPlanner.sqlite;Version=3;");
            m_dbConnection.Open();
            Console.WriteLine("Success!");
            Console.Read();
        }
	}
}
