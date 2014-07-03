using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class MessageModel
	{
		public int Id { get; set; }

		public int LayerId { get; set; }

		public string LayerDescription { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Omschrijving")]
		public string Description { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(0, 99, ErrorMessage = "Een geldige waarde voor het veld Positie X is tussen 0 en 99")]
		[Display(Name = "Positie X")]
		public int PositionX { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(0, 99, ErrorMessage = "Een geldige waarde voor het veld Positie Y is tussen 0 en 99")]
		[Display(Name = "Positie Y")]
		public int PositionY { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(1, 100, ErrorMessage = "Een geldige waarde voor het veld Hoogte is tussen 1 en 100")]
		[Display(Name = "Hoogte")]
		public int Height { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(1, 100, ErrorMessage = "Een geldige waarde voor het veld Breedte is tussen 1 en 100")]
		[Display(Name = "Breedte")]
		public int Width { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Display(Name = "Berichtsoort")]
		public string MessageKind { get; set; }

		public static MessageModel Create(MessageBoard.DAL.Entity.Message message, MessageBoard.DAL.Entity.Layer layer)
		{
			var result = new MessageModel();
			if (message != null)
			{
				result.Id = message.Id;
				result.Description = message.Description;
				result.PositionX = message.PositionX;
				result.PositionY = message.PositionY;
				result.Height = message.Height;
				result.Width = message.Width;
				result.MessageKind = message.MessageKind;
			}

			result.LayerId = layer.Id;
			result.LayerDescription = layer.Description;
			
			return result;
		}
	}
}