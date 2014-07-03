﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core
{
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
