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
            //dbHandler.CreateTables(true);
            dbHandler.addRecipe("Test", "Test", 1, false, false, false, false);
            dbHandler.addUnit("Test", "Test", 1);
            dbHandler.addNutrition("Test", 1, "Test", "Test", 1, 1, 1, 1, 1);
            dbHandler.addIngredient("Test", "Test", 1, "Test", "Test");
            Console.Read();
            dbHandler.Close();
        }
	}
}
