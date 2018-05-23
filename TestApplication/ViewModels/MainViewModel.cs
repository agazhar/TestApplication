using System.Collections.ObjectModel;
using TestApplication.Helpers;

namespace TestApplication.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<BaseViewModel> TabbedViewModels { get; private set; }

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; NotifyPropertyChanged(); }
        }
        public HeaderBarViewModel HeaderBarViewModel
        {
            get { return _headerBarViewModel; }
            set { _headerBarViewModel = value; NotifyPropertyChanged(); }
        }
        public DefaultViewModel DefaultViewModel
        {
            get { return _defaultViewModel; }
            set { _defaultViewModel = value; NotifyPropertyChanged(); }
        }

        public int CurrentTabIndex
        {
            get { return _currentTabIndex; }
            set { _currentTabIndex = value; NotifyPropertyChanged();}
        }
        
        private BaseViewModel _currentViewModel;
        private HeaderBarViewModel _headerBarViewModel;
        private DefaultViewModel _defaultViewModel;
        private int _currentTabIndex;
        
        public MainViewModel()
        {
            TabbedViewModels = new ObservableCollection<BaseViewModel>();
            HeaderBarViewModel = new HeaderBarViewModel();
            DefaultViewModel = new DefaultViewModel();
            CurrentViewModel = DefaultViewModel;

            InitialiseMediator();
            InitialiseTabbedViewModels();
        }
        
        public void InitialiseTabbedViewModels()
        {
            //Change for loop and list
            var mealViewModel = new MealViewModel() { header = "Saved Meals" };
            var ingredientViewModel = new IngredientViewModel() { header = "Saved Ingredient" };
            TabbedViewModels.Add(mealViewModel);
            TabbedViewModels.Add(ingredientViewModel);            
        }

        public void InitialiseMediator()
        {
            Mediator.Register("SwitchViewModel", SwitchViewModel);
        }

        public void SwitchViewModel(object args)
        {
            BaseViewModel newViewModel = (BaseViewModel)args;
            CurrentViewModel = newViewModel;
        }


    }
}
