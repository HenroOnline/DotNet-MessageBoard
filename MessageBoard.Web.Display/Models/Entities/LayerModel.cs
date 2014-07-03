using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Display.Models.Entities
{
	public class LayerModel
	{
		public int Columns { get; set; }

		public int Rows { get; set; }

		public int Sequence { get; set; }

		public List<MessageModel> Messages { get; set; }

		public static LayerModel Create(Layer layer)
		{
			var result = new LayerModel();

			result.Columns = layer.Columns;
			result.Rows = layer.Rows;
			result.Sequence = layer.Sequence;
			result.Messages = new List<MessageModel>();

			var colorIndex = 0;
			foreach (var message in MessageRepository.Instance.ListByLayer(layer.Id))
			{
				var opacity = layer.Sequence > 10 ? "0.5" : "1.0";
				var color = layer.Sequence <= 10 ? "D4726A" : Colors[colorIndex];
				result.Messages.Add(MessageModel.Create(message, color, opacity));

				colorIndex += 1;

				if (colorIndex == Colors.Count)
				{
					colorIndex = 0;
				}
			}

			return result;			
		}

		protected static List<string> Colors
		{
			get
			{
				return new List<string> { "3366FF", "6633FF", "CC33FF", "FF33CC",
																	"33CCFF", "003DF5", "002EB8", "FF3366",
																	"33FFCC", "B88A00", "F5B800", "FF6633",
																	"33FF66", "66FF33", "CCFF33", "FFCC33"};
			}
		}
	}
}