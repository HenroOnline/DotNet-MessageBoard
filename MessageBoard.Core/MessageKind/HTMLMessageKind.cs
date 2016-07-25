using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class HTMLMessageKind : MessageKind
	{
		public override string Title
		{
			get { return "HTML"; }
		}

		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("Title", "Titel", SettingKind.Text),
					MessageKindSetting.Create("Content", "Inhoud", SettingKind.FormattedText)
				};
			}
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, IInformationRepository informationRepository, string dataUrl)
		{
			var contentSetting = settings["Content"];
			var titleSetting = settings["Title"];

			if ((contentSetting == null
					|| string.IsNullOrEmpty(contentSetting.StringValue))
					&&
					(titleSetting == null
					|| string.IsNullOrEmpty(titleSetting.StringValue))
				)
			{
				// No data...
				return string.Empty;
			}

			if (titleSetting == null || string.IsNullOrEmpty(titleSetting.StringValue))
			{
				return contentSetting.StringValue;
			}

			var stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"panel panel-primary\">");
			stringBuilder.Append(string.Format("<div class=\"panel-heading\">{0}</div>", titleSetting.StringValue));			
			stringBuilder.Append("<div class=\"panel-body\">");
			stringBuilder.Append(contentSetting.StringValue);
			stringBuilder.Append("</div>");
			stringBuilder.Append("</div>");

			return stringBuilder.ToString();
		}
	}
}
