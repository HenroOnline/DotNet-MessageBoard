using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public class TextInformationKind : InformationKind
	{
		public override string Title
		{
			get { return "Tekst"; }
		}

		public override bool FreeContent
		{
			get { return true; }
		}
		
		public override string RenderHTML(InformationDataList informationData)
		{
			if (informationData.Count != 1)
			{
				return string.Empty;
			}

			return informationData[0].Content;
		}
	}
}
