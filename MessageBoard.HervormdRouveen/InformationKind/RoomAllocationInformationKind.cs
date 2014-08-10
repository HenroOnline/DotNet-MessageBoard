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

		public override string RenderHTML(InformationDataList informationData)
		{
			if (informationData.Count == 0)
			{
				return string.Empty;
			}

			var rowCount = informationData.Max(id => id.Row);

			var result = new StringBuilder();
			result.Append(string.Format("<table class=\"table {0}\"><thead></thead><tbody>", Key.Replace(".", "_")));
			for (var rowIndex = 0; rowIndex <= rowCount; rowIndex++)
			{
				var roomContentRecord = informationData.FirstOrDefault(id => id.Row == rowIndex && id.Column == 0);
				var allocationContentRecord = informationData.FirstOrDefault(id => id.Row == rowIndex && id.Column == 1);
				
				if ((roomContentRecord == null || string.IsNullOrEmpty(roomContentRecord.Content))
						&& (allocationContentRecord == null || string.IsNullOrEmpty(allocationContentRecord.Content)))
				{
					result.Append("<tr><th>&nbsp;</th><td>&nbsp;</td><td>&nbsp;</td></tr>");
					continue;
				}

				var roomContent = (roomContentRecord == null || string.IsNullOrEmpty(roomContentRecord.Content)) ? "&nbsp;" : roomContentRecord.Content;
				var allocationContent = (allocationContentRecord == null || string.IsNullOrEmpty(allocationContentRecord.Content)) ? "&nbsp;" : allocationContentRecord.Content;

				var remarkContentRecord = informationData.FirstOrDefault(id => id.Row == rowIndex && id.Column == 2);
				var remarkContent = (remarkContentRecord == null || string.IsNullOrEmpty(remarkContentRecord.Content)) ? "&nbsp;" : remarkContentRecord.Content;

				result.Append("<tr>");
				result.Append(string.Format("<th>{0}</th>", roomContent));
				result.Append(string.Format("<td>{0}</td>", allocationContent));
				result.Append(string.Format("<td>{0}</td>", remarkContent));
				result.Append("</tr>");				
			}
			result.Append("</tbody></table>");

			return result.ToString();
		}
	}
}
