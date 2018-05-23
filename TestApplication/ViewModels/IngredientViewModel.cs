using System.Collections.ObjectModel;
using TestApplication.Helpers;
using TestApplication.LINQClasses;

namespace TestApplication.ViewModels
{
    class IngredientViewModel : BaseViewModel
    {
        public ObservableCollection<string> IngredientList
        {
            get { return _ingredientList; }
            set
            {
                _ingredientList = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _ingredientList;

        public IngredientViewModel()
        {
            IngredientList = new ObservableCollection<string>();
            PopulateIngredientList();
        }

        public void RepopulateIngredientList(object args)
        {
            PopulateIngredientList();
            InitialiseMediator();
        }

        public void PopulateIngredientList()
        {
            IngredientList.Clear();
            MealPlan mealPlan = new MealPlan();
            foreach (Ingredient i in mealPlan.Ingredients)
            {
                IngredientList.Add(i.IngredientName);
            }

        }

        public void InitialiseMediator()
        {
            Mediator.Register("RepopulateIngredientList", RepopulateIngredientList);
        }
    }
}
