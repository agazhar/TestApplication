using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TestApplication.LINQClasses
{
    [Table(Name = "MealIngredient")]
    public class MealIngredient
    {
        // Set up a composite key by just setting both to PrimaryKeys :D
        // They are also foreign keys so set to private
        [Column(IsPrimaryKey = true)]
        public int IngredientID { get; set; }
        private EntityRef<Ingredient> _ingredient = new EntityRef<Ingredient>();

        [Association(Name = "FK_MealIngredient_Ingredient_Cascade", IsForeignKey = true, Storage = "_ingredient", ThisKey = "IngredientID")]
        public Ingredient Ingredient
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; }
        }

        [Column(IsPrimaryKey = true)]
        public int MealID { get; set; }
        private EntityRef<Meal> _meal = new EntityRef<Meal>();

        [Association(Name = "FK_MealIngredient_Meal_Cascade", IsForeignKey = true, Storage = "_meal", ThisKey = "MealID")]
        public Meal Meal
        {
            get { return _meal.Entity; }
            set { _meal.Entity = value; }
        }
    }
}
