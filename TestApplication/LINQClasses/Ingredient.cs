using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace TestApplication.LINQClasses
{
    [Table(Name = "Ingredient")]
    public class Ingredient
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int IngredientID { get; set; }

        private EntitySet<MealIngredient> _mealIngredients = new EntitySet<MealIngredient>();
        [Association(Name = "FK_MealIngredient_Ingredient_Cascade", Storage = "_mealIngredients", OtherKey = "IngredientID", ThisKey = "IngredientID")]
        private ICollection<MealIngredient> MealIngredients
        {
            get { return _mealIngredients; }
            set { _mealIngredients.Assign(value); }
        }

        public ICollection<Meal> Meals
        {
            get { return (from mi in MealIngredients select mi.Meal).ToList(); }
        }

        [Column] public string IngredientName { get; set; }
        [Column] public decimal PricePerPack { get; set; }
        //[Column] public int PackWeight { get; set; }
        [Column] public int NumberInPack { get; set; }
    }
}
