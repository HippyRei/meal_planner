using System.Data.SQLite;
using MealPlanner.Sql;

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

        public void CreateTables()
        {
            new SQLiteCommand(MealPlannerCreate.createRecipeTable, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerCreate.createUnitConversionTable, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerCreate.createNutritionTable, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerCreate.createIngredientTable, dbConnection).ExecuteNonQuery();
        }
        
        public void Close()
        {
            dbConnection.Close();
        }
    }
}
