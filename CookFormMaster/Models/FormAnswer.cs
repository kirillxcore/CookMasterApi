using System.Collections.Generic;

namespace CookFormMaster.Models
{
	public class FormAnswer
	{
		public string ResponseId { get; set; }

		public List<FormAnswerValue> FormValues { get; set; }
	}
}
