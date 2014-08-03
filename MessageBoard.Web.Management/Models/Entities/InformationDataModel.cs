using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class InformationDataModel
	{
		public int Column { get; set; }
		public int Row { get; set; }

		public string Content { get; set; }

		public static InformationDataModel Create (DAL.Entity.InformationData data)
		{
			var result = new InformationDataModel();
			if (data == null)
			{
				return result;
			}

			result.Column = data.Column;
			result.Row = data.Row;
			result.Content = data.Content;

			return result;
		}
	}
}