using System.Data.SQLite;

namespace MealPlanner
{
    class DatabaseHandler
    {
        private const string connectionStringFormat = "Data Source= {0};Version=3;";

        private SQLiteConnection dbConnection;
        public DatabaseHandler(string dbPath)
        {
            dbConnection = new SQLiteConnection(string.Format(connectionStringFormat, dbPath));
            dbConnection.Open();
        }
        
        ~DatabaseHandler()
        {
            dbConnection.Close();
        }
    }
}
