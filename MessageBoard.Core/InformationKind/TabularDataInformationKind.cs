using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public abstract class TabularDataInformationKind : InformationKind
	{
		public override string Title
		{
			get
			{
				if (ColumnCount == 1)
				{
					return "Tabel 1 kolom";
				}
				return string.Format("Tabel {0} koloms", ColumnCount);
			}
		}

		public override string SortKey
		{
			get
			{
				return string.Format("Tabel {0:000}", ColumnCount);
			}
		}

		public virtual int ColumnCount { get; private set; }

		public override bool TabularData
		{
			get { return true; }
		}

		public override List<InformationColumn> TabularDataColumns
		{
			get
			{
				var defaultColumn = new InformationColumn
				{
					Title = string.Empty,
					HorizontalAlignment = HorizontalAlignment.Left
				};

				var result = new List<InformationColumn>();
				for (var i = 0; i < ColumnCount; i++)
				{
					result.Add(defaultColumn);
				}

				return result;
			}
		}

		public override int TabularDataDefaultRows
		{
			get
			{
				return 10;
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
			result.Append(string.Format("<table class=\"{0}\">", Key));
			for (var rowIndex = 0; rowIndex < rowCount; rowIndex++ )
			{
				result.Append("<tr>");
				for (var columnIndex = 0; columnIndex < ColumnCount; columnIndex++)
				{					
					var columnData = informationData.FirstOrDefault(id => id.Row == rowIndex && id.Column == columnIndex);
					var additionalClass = string.Empty;
					var content = string.Empty;
					if (columnData != null)
					{
						content = columnData.Content;

						if (columnData.Highlight)
						{
							additionalClass = "highlight";
						}
						switch (columnData.HorizontalAlignment)
						{
							case HorizontalAlignment.Left:
								additionalClass += " left";
								break;
							case HorizontalAlignment.Center:
								additionalClass += " center";
								break;
							case HorizontalAlignment.Right:
								additionalClass += " right";
								break;
						}
					}

					if (!string.IsNullOrEmpty(additionalClass))
					{
						result.Append(string.Format("<td class=\"{0}\">", additionalClass));
					}
					else
					{
						result.Append("<td>");
					}

					result.Append(content);
					
					result.Append("</td>");
				}
				result.Append("</tr>");
			}
			result.Append("</table>");

			return result.ToString();
		}
	}
}
