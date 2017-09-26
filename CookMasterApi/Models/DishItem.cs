namespace CookMasterApi.Models
{
    public class DishItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsVegan { get; set; }
        public string ImageUrl { get; set; }
    }
}