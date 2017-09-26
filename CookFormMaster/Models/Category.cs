using System.Collections.Generic;

namespace CookFormMaster.Models
{
	public class Category
	{
		public string Title { get; set; }

		public List<Dish> Dishes { get; set; }
	}
}