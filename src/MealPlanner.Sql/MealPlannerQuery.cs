
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
    }
}
