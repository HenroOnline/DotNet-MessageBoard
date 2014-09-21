using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public class ImageListInformationKind : InformationKind
	{
		public override string Title
		{
			get { return "Afbeeldingen lijst"; }
		}

		public override bool TabularData
		{
			get
			{
				return true;
			}
		}

		public override List<InformationColumn> TabularDataColumns
		{
			get
			{
				var result = new List<InformationColumn>();

				result.Add(new InformationColumn("Afbeelding"));
				result.Add(new InformationColumn("Breedte"));
				result.Add(new InformationColumn("Hoogte"));

				return result;
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
			
			for (var rowIndex = 0; rowIndex <= rowCount; rowIndex++)
			{
				var imageUrl = informationData.FirstOrDefault(i => i.Row == rowIndex && i.Column == 0);
				if (imageUrl == null || string.IsNullOrWhiteSpace(imageUrl.Content))
				{
					continue;
				}

				var imageWidthData = informationData.FirstOrDefault(i => i.Row == rowIndex && i.Column == 1);
				var imageWidth = 0;
				var imageWidthAttribute = string.Empty;
				if (imageWidthData != null
						&& !string.IsNullOrWhiteSpace(imageWidthData.Content)
						&& int.TryParse(imageWidthData.Content, out imageWidth))
				{
					if (imageWidth > 0)
					{
						imageWidthAttribute = string.Format("max-width:{0}px;", imageWidth);
					}
				}


				var imageHeightData = informationData.FirstOrDefault(i => i.Row == rowIndex && i.Column == 2);
				var imageHeight = 0;
				var imageHeightAttribute = string.Empty;
				if (imageHeightData != null
						&& !string.IsNullOrWhiteSpace(imageHeightData.Content)
						&& int.TryParse(imageHeightData.Content, out imageHeight))
				{
					if (imageHeight > 0)
					{
						imageHeightAttribute = string.Format("max-height:{0}px;", imageHeight);
					}
				}

				result.Append(string.Format("<img src=\"{0}\" style=\"display: none;{1} {2}\">", imageUrl.Content, imageWidthAttribute, imageHeightAttribute));
			}

			return result.ToString();
		}
	}
}
