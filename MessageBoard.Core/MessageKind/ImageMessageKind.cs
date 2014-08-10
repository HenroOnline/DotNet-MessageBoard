using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class ImageMessageKind : MessageKind
	{
		public override string Title
		{
			get { return "Afbeelding"; }
		}

		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("ImageUrl", "Url", SettingKind.Text)
				};
			}
		}

		public override string RenderHTML(MessageKindSettingList settings, IInformationRepository informationRepository)
		{
			var imageUrl = settings["ImageUrl"];

			if (imageUrl == null || string.IsNullOrEmpty(imageUrl.StringValue))
			{
				// No data...
				return string.Empty;
			}

			var stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("<img src=\"{0}\" />", imageUrl.StringValue));

			return stringBuilder.ToString();
		}
	}
}
