namespace MealPlanner.Sql
{

    /// <summary>
    /// Class that contains all of the CREATE TABLE SQL commands.
    /// </summary>
    public static class MealPlannerSchema
    {
        /// <summary>
        /// Creates the unit conversion table. `from` is the unit of the value being converted, `to` is the unit
        /// the value is converted to, and `ratio` is the factor `from` is multiplied in order to become `to`.
        /// `ratio` must be positive.
        /// </summary>
        public const string unitConversionTableSchema = "CREATE TABLE `unit_conversion` (" +
                                                        "`unit` TEXT NOT NULL," +
                                                        "`type`	TEXT NOT NULL," +
                                                        "`ratio` REAL NOT NULL CHECK(`ratio` > 0)," +
                                                        "PRIMARY KEY(`unit`, `type`));";

        /// <summary>
        /// Creates the nutrition table. `ingredient_name` is the name of the ingredient, `quantity` is the number
        /// of units of the ingredient the nutritional information applies to, `unit` is the type of unit `quantity`
        /// applies to, `fat` is the amount of fat in grams, `carb` is the number of carbs in grams, `prot` is the
        /// amount of protein in grams, `fiber` is the amount of fiber in grams, and `sugar` is the amount of sugar
        /// in grams.
        /// All of the nutritional measurements must be greater than or equal to 0 and quanity must be positive.
        /// </summary>
        public const string createNutritionTable = "CREATE TABLE `nutrition` (" +
                                                    "`ingredient_name`	TEXT NOT NULL," +
                                                    "`quantity`	REAL NOT NULL CHECK(`quantity` > 0)," +
                                                    "`unit` TEXT NOT NULL," +
                                                    "`type` TEXT NOT NULL," +
                                                    "`fat`	REAL NOT NULL DEFAULT 0 CHECK(fat >= 0)," +
                                                    "`carb`	REAL NOT NULL DEFAULT 0 CHECK(carb >= 0)," +
                                                    "`prot`	REAL NOT NULL DEFAULT 0 CHECK(prot >= 0)," +
                                                    "`fiber` REAL NOT NULL DEFAULT 0 CHECK(fiber >= 0)," +
                                                    "`sugar` REAL NOT NULL DEFAULT 0 CHECK(sugar >= 0)," +
                                                    "FOREIGN KEY(`unit`, `type`) REFERENCES `unit_conversion`(`unit`, `type`)" +
                                                    "PRIMARY KEY(`ingredient_name`,`unit`, `type`));";

        /// <summary>
        /// Creates the recipe table. `recipe_name` is the name of the recipe, `servings` is the number of servings
        /// the recipe makes, `instructions` contains the instructions for making the recipe, `breakfast`, `lunch`, 
        /// `dinner`, and `snack` are set to 1 if the recipe belongs in the respective category, 0 otherwise.
        /// `servings` must be greater than 0 and all meal flags must be 0 or 1.
        /// </summary>
        public const string createRecipeTable = "CREATE TABLE `recipes` (" +
                                                "`recipe_name` TEXT NOT NULL," +
                                                "`instructions` TEXT," +
                                                "`servings` REAL NOT NULL CHECK(servings > 0)," +
                                                "`breakfast` INTEGER NOT NULL DEFAULT 0 CHECK(breakfast IN ( 0 , 1 ))," +
                                                "`lunch` INTEGER NOT NULL DEFAULT 0 CHECK(lunch IN ( 0 , 1 ))," +
                                                "`dinner` INTEGER NOT NULL DEFAULT 0 CHECK(dinner IN ( 0 , 1 ))," +
                                                "`snack` INTEGER NOT NULL DEFAULT 0 CHECK(snack IN ( 0 , 1 ))," +
                                                "PRIMARY KEY(`recipe_name`));";

        /// <summary>
        /// Creates the ingredients table. `recipe_name` is the name of the recipe, `ingredient_name` is the name of an
        /// ingredient of the recipe, `quantity` is the number of units of the ingredient are in the recipe, and `unit` is the
        /// type of unit the quantity applies to.
        /// `quantity` must be greater than 0.
        /// </summary>
        public const string createIngredientTable = "CREATE TABLE `ingredients` (" +
                                                    "`recipe_name` TEXT NOT NULL," +
                                                    "`ingredient_name` TEXT NOT NULL," +
                                                    "`quantity` REAL NOT NULL CHECK(quantity > 0)," +
                                                    "`unit` TEXT NOT NULL," +
                                                    "`type` TEXT NOT NULL," +
                                                    "FOREIGN KEY(`recipe_name`) REFERENCES `recipes`(`recipe_name`)," +
                                                    "FOREIGN KEY(`unit`, `type`) REFERENCES `unit_conversion`(`unit`, `type`)" +
                                                    "PRIMARY KEY(`recipe_name`, `ingredient_name`, `unit`));";

    }
}
