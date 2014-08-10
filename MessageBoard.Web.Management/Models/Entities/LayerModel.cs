using MessageBoard.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class LayerModel
	{
		public int Id { get; set; }

		public int SlideId { get; set; }

		public string SlideDescription { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Omschrijving")]
		public string Description { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(1, 100, ErrorMessage = "Een geldige waarde voor het veld kolommen is tussen 1 en 100")]
		[Display(Name = "Kolommen")]
		public int Columns { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Range(1, 100, ErrorMessage = "Een geldige waarde voor het veld rijen is tussen 1 en 100")]
		[Display(Name = "Rijen")]
		public int Rows { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Display(Name = "Z-index")]
		public int Sequence { get; set; }

		public static LayerModel Create(MessageBoard.DAL.Entity.Layer layer, MessageBoard.DAL.Entity.Slide slide)
		{
			var result = new LayerModel();
			if (layer != null)
			{
				result.Id = layer.Id;
				result.Description = layer.Description;
				result.Columns = layer.Columns;
				result.Rows = layer.Rows;
				result.Sequence = layer.Sequence;
			}

			result.SlideId = slide.Id;
			result.SlideDescription = slide.Description;				

			return result;
		}
	}
}