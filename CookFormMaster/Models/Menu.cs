using System.Collections.Generic;

namespace CookFormMaster.Models
{
    public class Menu
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<MenuDay> days { get; set; }
    }
}