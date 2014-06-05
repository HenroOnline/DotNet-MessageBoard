using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL
{
	public class MessageKind
	{
		public string Key { get; set; }

		public string Title { get; set; }

		public List<MessageKindSetting> Settings { get; set; }

		public static readonly MessageKind InformationText = new MessageKind
		{
			Key = "InformationText",
			Title = "Informatie blok",
			Settings = new List<MessageKindSetting>
			{
				MessageKindSetting.Create("Text", "Tekst", SettingKind.FormattedText)
			}
		};		
	}

	public class MessageKindSetting
	{
		public string Key { get; set; }

		public string Name { get; set; }

		public SettingKind SettingKind { get; set; }

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
