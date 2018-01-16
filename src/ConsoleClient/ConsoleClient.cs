using System;
using System.Data.SQLite;

namespace MealPlanner
{
	class ConsoleClient
	{
        const string path_to_db = "..\\..\\..\\Data\\MealPlanner.sqlite";
		static void Main(string[] args)
		{
            Console.WriteLine("Connecting to " + path_to_db);
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source= " + path_to_db + ";Version=3;");
            m_dbConnection.Open();
            Console.WriteLine("Connected successfully");

            m_dbConnection.Close();
        }
	}
}
