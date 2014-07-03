using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Slide
{
	public class IndexViewModel : BaseViewModel
	{
		public List<SlideModel> Slides { get; set; }

		public IndexViewModel()
		{
			Menu = "Slide";

			AddCrumblePath("Slides", "~/Slide");

			Slides = new List<SlideModel>();
		}

		public static IndexViewModel Create()
		{
			var result = new IndexViewModel();

			foreach (var s in SlideRepository.Instance.ListOrderedByDescription())
			{
				result.Slides.Add(new SlideModel
				{
					Id = s.Id,
					Description = s.Description					
				});
			}
			return result;
		}
	}
}