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
		public string Description { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string BackgroundColor { get; set; }

		public string Opacity { get; set; }

		public MvcHtmlString Value { get; set; }

		public static MessageModel Create(Message message, string backgroundColor, string opacity)
		{
			var result = new MessageModel();
			result.Description = message.Description;
			result.PositionX = message.PositionX;
			result.PositionY = message.PositionY;
			result.Width = message.Width;
			result.Height = message.Height;
			result.BackgroundColor = backgroundColor;
			result.Opacity = opacity;

			var messageKind = MessageBoard.Core.MessageKind.MessageKind.Select(message.MessageKind);
			var settings = SettingRepository.Instance.ListAsMessageKindSetting(message.Id);
			result.Value = MvcHtmlString.Create(messageKind.RenderHTML(settings, InformationDataRepository.Instance));

			return result;
		}
	}
}