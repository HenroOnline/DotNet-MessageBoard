using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Display.Models
{
	public class SlideModel
	{
		public int Columns { get; set; }

		public int Rows { get; set; }

		public List<MessageModel> Messages { get; set; }

		public SlideModel(Slide slide, List<Message> messages)
		{
			Columns = slide.Columns;
			Rows = slide.Rows;

			Messages = new List<MessageModel>();
			var counter = 0;
			foreach (var message in messages)
			{
				Messages.Add(new MessageModel(message, GetColor(counter)));

				counter += 1;
			}
		}

		public static string GetColor(int index)
		{
			string[] colorArray = { "27AAD7", "BCE236", "E88E27", "D72859", "2D4263", "27AAD7", "BCE236", "E88E27", "D72859", "2D4263"};

			return colorArray[index];
		}
	}
}