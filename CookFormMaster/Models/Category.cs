using System.Collections.Generic;

namespace CookFormMaster.Models
{
    public class Category
    {
        public string title { get; set; }
        public List<Dish> dishes { get; set; }
    }
}