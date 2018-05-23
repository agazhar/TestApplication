using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestApplication.Classes;
using TestApplication.Helpers;
using TestApplication.LINQClasses;

namespace TestApplication.ViewModels
{
    public class AddMealViewModel : BaseViewModel
    {
        public static readonly DependencyProperty IngredientsListProperty =
         DependencyProperty.Register("IngredientList", typeof(ObservableCollection<IngredientModel>), typeof(MainWindow), new UIPropertyMetadata(null));

        public ObservableCollection<IngredientModel> IngredientList
        {
            get { return _ingredientList; }
            set { _ingredientList = value; }
        }
        public ICommand ConfirmCommand
        {
            get { if (_confirmCommand == null) { _confirmCommand = new RelayCommand(ConfirmAddMeal); } return _confirmCommand; }
            set
            {
                _confirmCommand = value;
                NotifyPropertyChanged();
            }
        }
        public ICommand CancelCommand
        {
            get { if (_cancelCommand == null) { _cancelCommand = new RelayCommand(CancelAddMeal); } return _cancelCommand; }
            set
            {
                _cancelCommand = value;
                NotifyPropertyChanged();
            }
        }
        public string NameInput
        {
            get { return _nameInput; }
            set
            {
                _nameInput = value;
                NotifyPropertyChanged();
            }
        }
        public float PriceOfChecked
        {
            get { return _priceOfChecked; }
            set
            {
                _priceOfChecked = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<IngredientModel> _ingredientList;
        private ICommand _confirmCommand;
        private ICommand _cancelCommand;
        private string _nameInput;
        private float _priceOfChecked;

        public AddMealViewModel()
        {
            IngredientList = new ObservableCollection<IngredientModel>();
            PopulateIngredientList();
        }

        public void PopulateIngredientList()
        {
            IngredientList.Clear();
            MealPlan mealPlan = new MealPlan();
            foreach (Ingredient i in mealPlan.Ingredients)
            {
                IngredientModel item = new IngredientModel(i.IngredientName, i.IngredientID, i.PricePerPack, i.NumberInPack);
                IngredientList.Add(item);
            }
        }

        public void AddMealToDatabase(string name)
        {
            // TO-DO implement error handling. Check if already in database
            MealPlan mealPlan = new MealPlan();
            Meal newMeal = new Meal() { MealName = NameInput };
            mealPlan.Meals.InsertOnSubmit(newMeal);
            mealPlan.SubmitChanges();

            IEnumerable<Meal> chosenMeal = from meal in mealPlan.Meals
                                           where meal.MealName == NameInput
                                           select meal;

            foreach (var meal in chosenMeal)
            {
                foreach (IngredientModel im in IngredientList)
                {
                    if (im.IsChosen)
                    {
                        MealIngredient newMealIngredient = new MealIngredient() { MealID = meal.MealID, IngredientID = im.ID };
                        mealPlan.MealIngredients.InsertOnSubmit(newMealIngredient);
                        mealPlan.SubmitChanges();
                    }
                }
            }

        }

        public void ConfirmAddMeal(object o)
        {
            //Need to destroy on close as no longer needed
            if (!string.IsNullOrEmpty(NameInput))
            {
                AddMealToDatabase(NameInput);
                Mediator.NotifyColleagues("SwitchViewModel", new DefaultViewModel());
                Mediator.NotifyColleagues("RepopulateMealList", null);
            }
            else
            {
                MessageBox.Show("Please enter a name");
            }
        }

        public void CancelAddMeal(object o)
        {
            Mediator.NotifyColleagues("SwitchViewModel", new DefaultViewModel());
        }

        public decimal TotalPrice(List<IngredientModel> ingredientsIn)
        {
            // Unit test practise. Pretty unnecessary function...
            decimal total = 0;
            foreach (var p in ingredientsIn)
            {
                total += p.Price;
            }
            return total;
        }
    }
}
