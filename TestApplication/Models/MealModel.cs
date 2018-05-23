using TestApplication.Models;

namespace TestApplication
{
    public class MealModel : BaseModel
    {
        public string Name { get;set;}
        public int ID { get; set; }

        public MealModel(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
