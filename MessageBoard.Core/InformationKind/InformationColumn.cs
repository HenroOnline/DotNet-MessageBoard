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

		public InformationColumnKind ColumnKind { get; set; }

		public InformationColumn(string title = "", 
															HorizontalAlignment horizontalAlignment = Core.HorizontalAlignment.Left, 
															InformationColumnKind informationColumnKind = InformationColumnKind.Text)
		{			
			Title = title;
			HorizontalAlignment = horizontalAlignment;
			ColumnKind = informationColumnKind;
		}
	}
}
