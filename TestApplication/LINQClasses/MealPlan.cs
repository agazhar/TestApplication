using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;

namespace TestApplication.LINQClasses
{
    [Database]
    public class MealPlan : DataContext
    {
        public MealPlan() : base(new SqlConnection(ConfigurationManager.ConnectionStrings["MealPlanConnectionString"].ConnectionString))
        {
        }

        public Table<Ingredient> Ingredients;
        public Table<Meal> Meals;
        public Table<MealIngredient> MealIngredients;
    }


}
