namespace CookMasterApiModel
{
    public class DishItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsVegan { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string ImageUrlAlt { get; set; }
    }
}
