using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Display.Models
{
	public class MessageModel
	{
		public string Description { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public string BackgroundColor { get; set; }

		public MessageModel(Message message, string backgroundColor)
		{
			Description = message.Description;
			PositionX = message.PositionX;
			PositionY = message.PositionY;
			Width = message.Width;
			Height = message.Height;
			BackgroundColor = backgroundColor;
		}
	}
}