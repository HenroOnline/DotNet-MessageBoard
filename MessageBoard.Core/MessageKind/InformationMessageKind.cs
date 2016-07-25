using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class InformationMessageKind : MessageKind
	{
		public override string Title
		{
			get { return "Informatie"; }
		}

		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("Title", "Titel", SettingKind.Text),
					MessageKindSetting.Create("InformationHeader", "Informatie item", SettingKind.Information)					
				};
			}
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, IInformationRepository informationRepository, string dataUrl)
		{
			var informationHeaderSetting = settings["InformationHeader"];
			if (informationHeaderSetting == null || informationHeaderSetting.IntValue == 0)
			{
				return string.Empty;
			}

			var informationKind = informationRepository.RetrieveInformationKind(informationHeaderSetting.IntValue);
			if (informationKind == null)
			{
				return string.Empty;
			}

			var title = string.Empty;
			var titleSetting = settings["Title"];
			if (titleSetting != null && !string.IsNullOrEmpty(titleSetting.StringValue))
			{
				title = titleSetting.StringValue;
			}

			var informationData = informationRepository.RetrieveData(informationHeaderSetting.IntValue);

			var stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"panel panel-primary\">");
			if (!string.IsNullOrEmpty(title))
			{
				stringBuilder.Append(string.Format("<div class=\"panel-heading\">{0}</div>", title));
			}
			
			//stringBuilder.Append("<div class=\"panel-body\">");
			stringBuilder.Append(informationKind.RenderHTML(informationData));
			//stringBuilder.Append("</div>");
			stringBuilder.Append("</div>");

			return stringBuilder.ToString();
		}
	}
}
