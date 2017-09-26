using System.Collections.Generic;

namespace CookFormMaster.Models
{
	public class Menu
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public List<MenuDay> Days { get; set; }

		public Wishes Wishes { get; set; }
	}
}