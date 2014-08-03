using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.HervormdRouveen.InformationKind
{
	public class RoomAllocationInformationKind : MessageBoard.Core.InformationKind.InformationKind
	{
		public override string Title
		{
			get { return "Zaal indeling"; }
		}

		public override bool TabularData
		{
			get
			{
				return true;
			}
		}

		public override List<Core.InformationKind.InformationColumn> TabularDataColumns
		{
			get
			{
				return new List<InformationColumn>
				{
					new InformationColumn("Zaal"),
					new InformationColumn("Activiteit"),
					new InformationColumn("Opmerking")
				};
			}
		}
	}
}
