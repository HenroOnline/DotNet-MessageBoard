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
					MessageKindSetting.Create("InformationHeader", "Content", SettingKind.Information)
				};
			}
		}
	}
}
