using System;

namespace CookMasterApi.Models
{
    public class DishItemStat
    {
        public DishItem Item { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}