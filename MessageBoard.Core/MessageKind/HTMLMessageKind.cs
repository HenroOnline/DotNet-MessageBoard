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
					MessageKindSetting.Create("Content", "Inhoud", SettingKind.FormattedText),
				};
			}
		}
	}
}
