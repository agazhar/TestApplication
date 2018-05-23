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
    class AddIngredientViewModel : BaseViewModel
    {
        public string NameInput
        {
            get { return _nameInput; }
            set { _nameInput = value; NotifyPropertyChanged(); }
        }
        public decimal PriceInput
        {
            get { return _priceInput; }
            set { _priceInput = value; NotifyPropertyChanged(); }
        }
        public int PackWeightInput
        {
            get { return _packWeightInput; }
            set { _packWeightInput = value; NotifyPropertyChanged(); }
        }
        public int NoInPackInput
        {
            get { return _noInPackInput; }
            set { _noInPackInput = value; NotifyPropertyChanged(); }
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

        private string _nameInput;
        private decimal _priceInput;
        private int _packWeightInput;
        private int _noInPackInput;
        private ICommand _confirmCommand;
        private ICommand _cancelCommand;

        public AddIngredientViewModel()
        {

        }

        public void AddIngredientToDataBase(IngredientModel newIngred)
        {
            // TO-DO implement error handling. Check if already in database
            //Need to destroy on close as no longer using.
            MealPlan mealPlan = new MealPlan();
            Ingredient newIngredient = new Ingredient() { IngredientName = newIngred.Name };
            mealPlan.Ingredients.InsertOnSubmit(newIngredient);
            mealPlan.SubmitChanges();
            Mediator.NotifyColleagues("SwitchViewModel", new DefaultViewModel());
        }

        public void ConfirmAddMeal(object o)
        {
            if (!string.IsNullOrEmpty(NameInput))
            {
                AddIngredientToDataBase(new IngredientModel(NameInput, 0, PriceInput, NoInPackInput));
                Mediator.NotifyColleagues("RepopulateIngredientList", null);
                Mediator.NotifyColleagues("SwitchViewModel", new DefaultViewModel());
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
    }
}
