using System.Data.SQLite;
using System.IO;
using MealPlanner.Sql;

namespace MealPlanner.Common
{
    /// <summary>
    /// A class that handles interacting with the database.
    /// </summary>
    public class DatabaseHandler
    {
        /// <summary>
        /// Format of the connection string for the given SQLite database
        /// </summary>
        private const string connectionStringFormat = "Data Source= {0};Version=3;";

        /// <summary>
        /// The maintained connection to the database.
        /// </summary>
        private SQLiteConnection dbConnection;

        /// <summary>
        /// Creates a new instance of the DatabaseHandler class for a particular database.
        /// </summary>
        /// <param name="dbPath">Path to the database to be connected to</param>
        /// <exception cref="FileNotFoundException">Thrown if the database at the specified path does not exist</exception>
        public DatabaseHandler(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException(string.Format("Database {0} was not found.", dbPath));
            }
            dbConnection = new SQLiteConnection(string.Format(connectionStringFormat, dbPath));
            dbConnection.Open();
        }
        /// <summary>
        /// Closes the connection with the database.
        /// </summary>
        public void Close()
        {
            dbConnection.Close();
        }

        /// <summary>
        /// Creates all necessary tables in the database.
        /// </summary>
        /// <param name="overwrite">If true is passed, former tables will be dropped. Otherwise, an exception
        /// will be thrown if the table already exists.</param>
        public void CreateTables(bool overwrite)
        {
            if (overwrite)
            {
                dropIfExists(SqlConstants.ingredientTableName);
                dropIfExists(SqlConstants.nutritionTableName);
                dropIfExists(SqlConstants.recipeTableName);
                dropIfExists(SqlConstants.unitConversionTableName);
            }
            new SQLiteCommand(MealPlannerSchema.createRecipeTable, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerSchema.unitConversionTableSchema, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerSchema.createNutritionTable, dbConnection).ExecuteNonQuery();
            new SQLiteCommand(MealPlannerSchema.createIngredientTable, dbConnection).ExecuteNonQuery();
        }

        /// <summary>
        /// Checks if a given table exists in the database.
        /// </summary>
        /// <param name="tableName">The name of the table</param>
        /// <returns>True if the table exists, false otherwise</returns>
        public bool TableExists(string tableName)
        {
            SQLiteCommand getTable = new SQLiteCommand(MealPlannerQuery.doesTableExist, dbConnection);
            getTable.Parameters.AddWithValue("$table_name", tableName);
            return getTable.ExecuteReader().Read();
        }

        /// <summary>
        /// Drops the given table if it exists.
        /// </summary>
        /// <param name="tableName">The name of the table to drop</param>
        private void dropIfExists(string tableName)
        {
            SQLiteCommand dropTable = new SQLiteCommand(string.Format(MealPlannerQuery.dropTableIfExists, tableName), dbConnection);
            dropTable.ExecuteNonQuery();
        }
    }
}
