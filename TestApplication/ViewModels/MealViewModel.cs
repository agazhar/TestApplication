using System.Collections.ObjectModel;
using TestApplication.Helpers;
using TestApplication.LINQClasses;

namespace TestApplication.ViewModels
{
    class MealViewModel : BaseViewModel
    {
        public MealModel SelectedMeal
        {
            get { return _selectedMeal; }
            set { _selectedMeal = value; NotifyPropertyChanged(); Mediator.NotifyColleagues("SwitchViewModel", new MealPropertyViewModel(SelectedMeal)); }
        }
        public ObservableCollection<MealModel> MealList
        {
            get { return _mealList; }
            set
            {
                _mealList = value; NotifyPropertyChanged();
            }
        }

        private ObservableCollection<MealModel> _mealList;
        private MealModel _selectedMeal;

        public MealViewModel()
        {
            MealList = new ObservableCollection<MealModel>();
            PopulateMealList();
            InitialiseMediator();
        }

        public void RepopulateMealList(object args)
        {
            PopulateMealList();
        }

        public void PopulateMealList()
        {
            MealList.Clear();
            MealPlan mealPlan = new MealPlan();
            foreach (Meal m in mealPlan.Meals)
            {
                MealList.Add(new MealModel(m.MealName, m.MealID));
            }
        }

        public void InitialiseMediator()
        {
            Mediator.Register("RepopulateMealList", RepopulateMealList);
        }
    }
}
