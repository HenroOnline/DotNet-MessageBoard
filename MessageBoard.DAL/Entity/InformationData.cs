using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class InformationData : Base
	{
		public InformationHeader InformationHeader { get; set; }
		public int InformationHeaderId { get; set; }

		public string Content { get; set; }

		public int Column { get; set; }

		public int Row { get; set; }
	}
}
