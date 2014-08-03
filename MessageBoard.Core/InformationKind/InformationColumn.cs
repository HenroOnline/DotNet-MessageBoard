using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public class InformationColumn
	{
		public string Title { get; set; }

		public HorizontalAlignment HorizontalAlignment { get; set; }

		public InformationColumn() : this(string.Empty)
		{

		}

		public InformationColumn(string title) : this(title, HorizontalAlignment.Left)
		{

		}
		public InformationColumn(string title, HorizontalAlignment horizontalAlignment )
		{
			Title = title;
			HorizontalAlignment = horizontalAlignment;
		}
	}
}
