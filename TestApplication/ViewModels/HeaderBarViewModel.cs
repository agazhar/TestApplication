using System.Windows.Input;
using TestApplication.Classes;
using TestApplication.Helpers;

namespace TestApplication.ViewModels
{
    class HeaderBarViewModel : BaseViewModel
    {
        public ICommand SwitchToAddMealCommand
        {
            get
            {
                if (_addNewMealCommand == null)
                {
                    _addNewMealCommand = new RelayCommand(SwitchToAddMeal);
                }
                return _addNewMealCommand;
            }
            set
            {
                _addNewMealCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SwitchToAddIngredientCommand
        {
            get
            {
                if (_addNewIngredientCommand == null)
                {
                    _addNewIngredientCommand = new RelayCommand(SwitchToAddIngredient);
                }
                return _addNewIngredientCommand;
            }
            set
            {
                _addNewIngredientCommand = value;
                NotifyPropertyChanged();
            }
        }

        private ICommand _addNewMealCommand;
        private ICommand _addNewIngredientCommand;

        public HeaderBarViewModel()
        {

        }

        public void SwitchToAddMeal(object obj)
        {
            SwitchViewModel(new AddMealViewModel());
        }

        public void SwitchToAddIngredient(object obj)
        {
            SwitchViewModel(new AddIngredientViewModel());
        }

        public void SwitchViewModel(BaseViewModel newViewModel)
        {
            Mediator.NotifyColleagues("SwitchViewModel", newViewModel);
        }
    }
}
