using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Models.Entities
{
	public class LayerModel
	{
		public int Columns { get; set; }

		public int Rows { get; set; }

		public int Sequence { get; set; }

		public List<MessageModel> Messages { get; set; }

		public static LayerModel Create(Layer layer, UrlHelper urlHelper)
		{
			var result = new LayerModel();

			result.Columns = layer.Columns;
			result.Rows = layer.Rows;
			result.Sequence = layer.Sequence;
			result.Messages = new List<MessageModel>();

			foreach (var message in MessageRepository.Instance.ListByLayer(layer.Id))
			{
				result.Messages.Add(MessageModel.Create(message, urlHelper));			
			}

			return result;			
		}		
	}
}