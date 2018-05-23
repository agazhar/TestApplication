using TestApplication.Models;

namespace TestApplication
{
    public class IngredientModel : BaseModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public decimal Price { get; set; }
        public int NoInPack { get; set; }
        public bool IsChosen
        {
            get { return _isChosen; }
            set
            {
                _isChosen = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isChosen;

        public IngredientModel(string name, int id, decimal price, int noInPack, bool isChosen = false)
        {
            Name = name;
            ID = id;
            Price = price;
            NoInPack = noInPack;
            IsChosen = isChosen;

        }
    }
}
