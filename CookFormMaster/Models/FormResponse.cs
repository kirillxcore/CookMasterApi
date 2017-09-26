using System.Collections.Generic;

namespace CookFormMaster.Models
{
	public class FormResponse
	{
		public string ResponseId { get; set; }

		public List<FormValue> FormValues { get; set; }
	}
}
