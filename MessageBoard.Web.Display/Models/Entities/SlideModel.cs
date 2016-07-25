using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Models.Entities
{
	public class SlideModel
	{
		public List<LayerModel> Layers { get; set; }

		public bool FirstSlide { get; set; }

		public int Duration { get; set; }

		public static SlideModel Create(BoardSlide boardSlide, UrlHelper urlHelper)
		{
			var result = new SlideModel();
			result.Layers = new List<LayerModel>();
			result.Duration = boardSlide.Duration;

			foreach (var layer in LayerRepository.Instance.ListBySlide(boardSlide.Slide.Id))
			{
				result.Layers.Add(LayerModel.Create(layer, urlHelper));
			}

			return result;
		}
	}
}