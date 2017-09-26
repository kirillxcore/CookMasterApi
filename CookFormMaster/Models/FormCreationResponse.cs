using System.Collections.Generic;

namespace CookFormMaster.Models
{
	public class FormCreationResponse
	{
		public string PublishUrl { get; set; }
		
		public string FormId { get; set; }

		public int FeedBackId { get; set; }

		public List<RelationBetweenIds> RelationsBetweenIds { get; set; }
	}
}
