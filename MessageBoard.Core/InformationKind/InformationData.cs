using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public class InformationData
	{
		public int Column { get; set; }

		public int Row { get; set; }

		public string Content { get; set; }

		public HorizontalAlignment HorizontalAlignment { get; set; }

		public bool Highlight { get; set; }
	}
}
