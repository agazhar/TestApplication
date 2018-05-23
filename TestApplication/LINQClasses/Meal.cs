using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace TestApplication.LINQClasses
{
    [Table(Name = "Meal")]
    public class Meal
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int MealID { get; set; }

        private EntitySet<MealIngredient> _mealIngredients = new EntitySet<MealIngredient>();
        [Association(Name = "FK_MealIngredient_Meal_Cascade", Storage = "_mealIngredients", OtherKey = "MealID", ThisKey = "MealID")]
        private ICollection<MealIngredient> MealIngredients
        {
            get { return _mealIngredients; }
            set { _mealIngredients.Assign(value); }
        }

        public ICollection<Ingredient> Ingredients
        {
            get { return (from mi in MealIngredients select mi.Ingredient).ToList(); }
        }

        [Column] public string MealName { get; set; }
    }
}
