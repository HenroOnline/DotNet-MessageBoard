using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Display.Models.Entities
{
	public class SlideModel
	{
		public List<LayerModel> Layers { get; set; }

		public static SlideModel Create(Slide slide)
		{
			var result = new SlideModel();
			result.Layers = new List<LayerModel>();
			
			foreach (var layer in LayerRepository.Instance.ListBySlide(slide.Id))
			{
				result.Layers.Add(LayerModel.Create(layer));
			}

			return result;
		}
	}
}