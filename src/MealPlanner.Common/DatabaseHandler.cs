using System.Data.SQLite;
using System.IO;
using MealPlanner.Sql;
using System;

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
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.doesTableExist, dbConnection);
            sql.Parameters.AddWithValue("$table_name", tableName);
            return sql.ExecuteReader().Read();
        }

        public float GetRecipeFatPerServing(string recipeName)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.getRecipeFatPerServing, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            SQLiteDataReader reader = sql.ExecuteReader();
            reader.Read();
            return reader.GetFloat(0);
        }

        public bool RecipeExists(string recipeName)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.getRecipe, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            return sql.ExecuteReader().Read();
        }

        public bool IngredientExists(string recipeName, string ingredientName, string unit)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.getIngredient, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            sql.Parameters.AddWithValue("$ingredient_name", ingredientName);
            sql.Parameters.AddWithValue("$unit", unit);
            return sql.ExecuteReader().Read();
        }

        public bool NutritionExists(string ingredientName, string unit, string type)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.getNutrition, dbConnection);
            sql.Parameters.AddWithValue("$ingredient_name", ingredientName);
            sql.Parameters.AddWithValue("$unit", unit);
            sql.Parameters.AddWithValue("$type", type);
            return sql.ExecuteReader().Read();
        }

        public bool UnitConversionExists(string recipeName)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.getUnitConversion, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            return sql.ExecuteReader().Read();
        }

        public bool addRecipe(string recipeName, string instructions, float servings, bool breakfast, bool lunch, bool dinner, bool snack)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.insertIntoRecipes, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            sql.Parameters.AddWithValue("$instructions", instructions);
            sql.Parameters.AddWithValue("$servings", servings);
            sql.Parameters.AddWithValue("$breakfast", breakfast);
            sql.Parameters.AddWithValue("$lunch", lunch);
            sql.Parameters.AddWithValue("$dinner", dinner);
            sql.Parameters.AddWithValue("$snack", snack);
            try
            {
                sql.ExecuteNonQuery();
            } catch (SQLiteException)
            {
                return false;
            }
            return true;
        }

        public bool addIngredient(string recipeName, string ingredientName, float quantity, string unit, string type)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.insertIntoIngredients, dbConnection);
            sql.Parameters.AddWithValue("$recipe_name", recipeName);
            sql.Parameters.AddWithValue("$ingredient_name", ingredientName);
            sql.Parameters.AddWithValue("$quantity", quantity);
            sql.Parameters.AddWithValue("$unit", unit);
            sql.Parameters.AddWithValue("$type", type);
            try
            {
                sql.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                return false;
            }
            return true;
        }

        public bool addNutrition(string ingredientName, float quantity, string unit, string type, float fat, float carb, float prot, float fiber, float sugar)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.insertIntoNutrition, dbConnection);
            sql.Parameters.AddWithValue("$ingredient_name", ingredientName);
            sql.Parameters.AddWithValue("$quantity", quantity);
            sql.Parameters.AddWithValue("$unit", unit);
            sql.Parameters.AddWithValue("$type", type);
            sql.Parameters.AddWithValue("$fat", fat);
            sql.Parameters.AddWithValue("$carb", carb);
            sql.Parameters.AddWithValue("$prot", prot);
            sql.Parameters.AddWithValue("$fiber", fiber);
            sql.Parameters.AddWithValue("$sugar", sugar);
            try
            {
                sql.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                return false;
            }
            return true;
        }

        public bool addUnit(string unit, string type, float ratio)
        {
            SQLiteCommand sql = new SQLiteCommand(MealPlannerQuery.insertIntoUnit_Conversion, dbConnection);
            sql.Parameters.AddWithValue("$unit", unit);
            sql.Parameters.AddWithValue("$type", type);
            sql.Parameters.AddWithValue("$ratio", ratio);
            try
            {
                sql.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Drops the given table if it exists.
        /// </summary>
        /// <param name="tableName">The name of the table to drop</param>
        private void dropIfExists(string tableName)
        {
            SQLiteCommand sql = new SQLiteCommand(string.Format(MealPlannerQuery.dropTableIfExists, tableName), dbConnection);
            sql.ExecuteNonQuery();
        }
    }
}
