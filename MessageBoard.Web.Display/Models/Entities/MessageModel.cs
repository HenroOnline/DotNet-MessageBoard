using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Models.Entities
{
	public class MessageModel
	{
		public string Key { get; set; }

		public string Description { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string GlobalScript { get; set; }

		public string Styling { get; set; }

		public string InstanceScript { get; set; }
		
		public MvcHtmlString Value { get; set; }

		public static MessageModel Create(Message message, UrlHelper urlHelper)
		{
			var result = new MessageModel();
			result.Description = message.Description;
			result.PositionX = message.PositionX;
			result.PositionY = message.PositionY;
			result.Width = message.Width;
			result.Height = message.Height;

			var messageKind = MessageBoard.Core.MessageKind.MessageKind.Select(message.MessageKind);
			if (messageKind != null)
			{
				var settings = SettingRepository.Instance.ListAsMessageKindSetting(message.Id);

				var dataUrl = urlHelper.Action("GetData", "Data", new { MessageId = message.Id });

				result.Key = messageKind.Key;
				result.GlobalScript = messageKind.RenderGlobalScript(dataUrl);
				result.Styling = messageKind.RenderStyling();
				result.InstanceScript = messageKind.RenderInstanceScript(message.Id, settings, dataUrl);
				result.Value = MvcHtmlString.Create(messageKind.RenderHTML(message.Id, settings, InformationDataRepository.Instance, dataUrl));
			}
			return result;
		}
	}
}