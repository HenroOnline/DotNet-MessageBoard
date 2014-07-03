
using MessageBoard.Core.MessageKind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class MessageKindModel
	{
		public string Key { get; set; }

		public List<SettingModel> Settings { get; set; }

		public static MessageKindModel Create(MessageKind messageKind, int messageId, bool fetchSettingValues)
		{
			var result = new MessageKindModel();

			result.Key = messageKind.Key;
			result.Settings = new List<SettingModel>();
			foreach (var setting in messageKind.Settings)
			{
				result.Settings.Add(SettingModel.Create(setting, messageId, fetchSettingValues));
			}

			return result;
		}
	}
}