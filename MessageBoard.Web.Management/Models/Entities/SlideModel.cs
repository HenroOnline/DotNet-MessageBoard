using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class SlideModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Omschrijving")]
		public string Description { get; set; }

		public static SlideModel Create(DAL.Entity.Slide slide)
		{
			var result = new SlideModel();
			if (slide == null)
			{
				return result;
			}

			result.Id = slide.Id;
			result.Description = slide.Description;

			return result;
		}
	}
}