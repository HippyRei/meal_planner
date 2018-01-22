
namespace MealPlanner.Sql
{
    /// <summary>
    /// Contains all SQL queries that can be run on the database.
    /// </summary>
    public static class MealPlannerQuery
    {
        /// <summary>
        /// SQL query to see if a table exists. $table_name is the name of the table to be checked for.
        /// </summary>
        public const string doesTableExist = "SELECT name FROM sqlite_master WHERE type='table' AND name=$table_name;";

        /// <summary>
        /// Drops a given table if it exists. The name of the table must be overridden with string.Format.
        /// </summary>
        public static string dropTableIfExists = "DROP TABLE IF EXISTS {0};";

        public static string insertIntoRecipes = "INSERT INTO recipes(" +
                                                    "recipe_name, " +
                                                    "instructions, " +
                                                    "servings, " +
                                                    "breakfast, " +
                                                    "lunch, " +
                                                    "dinner, " +
                                                    "snack) " +
                                                    "VALUES(" +
                                                    "$recipe_name, " +
                                                    "$instructions, " +
                                                    "$servings, " +
                                                    "$breakfast, " +
                                                    "$lunch, " +
                                                    "$dinner, " +
                                                    "$snack);";

        public static string insertIntoIngredients = "INSERT INTO ingredients(" +
                                                    "recipe_name, " +
                                                    "ingredient_name, " +
                                                    "quantity, " +
                                                    "unit, " +
                                                    "type) " +
                                                    "VALUES(" +
                                                    "$recipe_name, " +
                                                    "$ingredient_name, " +
                                                    "$quantity, " +
                                                    "$unit, " +
                                                    "$type);";

        public static string insertIntoNutrition = "INSERT INTO nutrition(" +
                                                    "ingredient_name, " +
                                                    "quantity, " +
                                                    "unit, " +
                                                    "type, " +
                                                    "fat, " +
                                                    "carb, " +
                                                    "prot, " +
                                                    "fiber, " +
                                                    "sugar) " +
                                                    "VALUES(" +
                                                    "$ingredient_name, " +
                                                    "$quantity, " +
                                                    "$unit, " +
                                                    "$type, " +
                                                    "$fat, " +
                                                    "$carb, " +
                                                    "$prot, " +
                                                    "$fiber, " +
                                                    "$sugar);";

        public static string insertIntoUnit_Conversion = "INSERT INTO unit_conversion(" +
                                                    "unit, " +
                                                    "type, " +
                                                    "ratio) " +
                                                    "VALUES(" +
                                                    "$unit, " +
                                                    "$type, " +
                                                    "$ratio);";

        public static string getRecipeFatPerServing = "SELECT SUM(n.fat) AS tot_fat " +
                                            "FROM (SELECT * FROM ingredients WHERE recipe_name = $recipe_name) AS i " +
                                            "INNER JOIN nutrition n ON i.ingredient_name = n.ingredient_name " +
                                                "AND i.type = n.type " +
                                            "INNER JOIN unit_conversion f ON i.unit = f.unit " +
                                            "INNER JOIN unit_conversion t ON t.unit = n.unit;"
                                            ;

        public static string getRecipe = "SELECT * FROM recipes WHERE recipe_name = $recipe_name;";

        public static string getIngredient = "SELECT * FROM ingredients WHERE recipe_name = $recipe_name " +
                                             "AND ingredient_name = $ingredient_name AND unit = $unit;";

        public static string getNutrition = "SELECT * FROM nutrition WHERE ingredient_name = $ingredient_name AND unit = $unit AND type = $type;";

        public static string getUnitConversion = "SELECT * FROM unit_conversion WHERE unit = $unit AND type = $type;";
    }
}
