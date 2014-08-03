using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class MessageKindSetting
	{
		public string Key { get; set; }

		public string Name { get; set; }

		public SettingKind SettingKind { get; set; }

		public object Value
		{
			get;
			set;
		}

		public string StringValue
		{
			get
			{
				return (string)Value;
			}
		}

		public static MessageKindSetting Create(string key, string name, SettingKind settingKind)
		{
			return new MessageKindSetting
			{
				Key = key,
				Name = name,
				SettingKind = settingKind
			};
		}
	}
}
