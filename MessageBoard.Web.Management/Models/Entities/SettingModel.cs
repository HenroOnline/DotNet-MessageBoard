using MessageBoard.BLL.Repositories;
using MessageBoard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class SettingModel
	{
		public string Key { get; set; }

		public string Name { get; set; }

		public SettingKind Kind { get; set; }

		public string Value { get; set; }

		public static SettingModel Create(MessageKindSetting setting, int messageId, bool fetchSettingValues)
		{
			var result = new SettingModel();

			result.Key = setting.Key;
			result.Name = setting.Name;
			result.Kind = setting.SettingKind;			

			if (!fetchSettingValues)
			{
				return result;
			}

			var settingValue = SettingRepository.Instance.Select(messageId, result.Key);
			if (settingValue != null)
			{
				result.Value = settingValue.StringValue;
			}

			return result;
		}
	}
}