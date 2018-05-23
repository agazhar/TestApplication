using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestApplication.Classes;
using TestApplication.Helpers;
using TestApplication.LINQClasses;

namespace TestApplication.ViewModels
{
    public class MealPropertyViewModel : BaseViewModel
    {
        public MealModel MealModel
        {
            get { return _mealModel; }
            set
            {
                _mealModel = value;
                NotifyPropertyChanged();
            }
        }
        public List<IngredientModel> IngredientList
        {
            get { return _ingredientList; }
            set
            {
                _ingredientList = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand DeleteCommand
        {
            get { if (_deleteCommand == null) { _deleteCommand = new RelayCommand(DeleteMeal); } return _deleteCommand; }
            set { _deleteCommand = value; NotifyPropertyChanged(); }
        }

        private MealModel _mealModel;
        private List<IngredientModel> _ingredientList;
        private ICommand _deleteCommand;

        public MealPropertyViewModel(MealModel m)
        {
            MealModel = m;
            IngredientList = new List<IngredientModel>();
            PopulateIngredientList(m);
        }

        public void PopulateIngredientList(MealModel m)
        {
            IngredientList.Clear();
            if (m != default(MealModel))
            {
                MealPlan mealPlan = new MealPlan();
                IEnumerable<Meal> chosenMeal = from meal in mealPlan.Meals
                                               where meal.MealName == m.Name
                                               select meal;

                foreach (var meal in chosenMeal)
                {
                    ICollection<Ingredient> chosenIngredients = meal.Ingredients;
                    foreach (var ci in chosenIngredients)
                    {
                        IngredientModel im = new IngredientModel(ci.IngredientName, ci.IngredientID, ci.PricePerPack, ci.NumberInPack);
                        IngredientList.Add(im);
                    }
                }
            }
        }

        public void DeleteMeal(object o)
        {
            MealPlan mealPlan = new MealPlan();
            Meal meal = mealPlan.Meals.Single(m => m.MealName == MealModel.Name);
            mealPlan.Meals.DeleteOnSubmit(meal);
            mealPlan.SubmitChanges();

            Mediator.NotifyColleagues("RepopulateMealList", null);
            Mediator.NotifyColleagues("RepopulateIngredientList", null);
            Mediator.NotifyColleagues("SwitchViewModel", new DefaultViewModel());
        }


    }
}
