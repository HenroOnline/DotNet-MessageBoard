using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Slide
{
	public class DetailViewModel : BaseViewModel
	{
		public SlideModel Slide { get; set; }

		public List<LayerModel> Layers { get; set; }

		public DetailViewModel()
		{			
			Menu = "Slide";
		}

		public static DetailViewModel Create(int id)
		{
			var result = new DetailViewModel();
			
			var dbSlide = SlideRepository.Instance.Select(id);
			result.Slide = SlideModel.Create(dbSlide);
			result.Layers = new List<LayerModel>();
			foreach (var layer in LayerRepository.Instance.ListBySlide(result.Slide.Id))
			{
				result.Layers.Add(LayerModel.Create(layer, dbSlide));
			}

			result.AddCrumblePath("Slides", "~/Slide");
			result.AddCrumblePath((result.Slide.Id == 0) ? "Slide toevoegen" : result.Slide.Description, string.Format("~/Slide/", result.Slide.Id));

			return result;
		}

		public void Save()
		{
			DAL.Entity.Slide dbSlide = null;
			if (Slide.Id != 0)
			{
				dbSlide = SlideRepository.Instance.Select(Slide.Id);
			}

			if (dbSlide == null)
			{
				dbSlide = new DAL.Entity.Slide();
			}

			dbSlide.Description = Slide.Description;
			
			SlideRepository.Instance.Save(dbSlide);

			Slide.Id = dbSlide.Id;
		}

		public void Delete()
		{
			var dbSlide = SlideRepository.Instance.Select(Slide.Id);
			if (dbSlide != null)
			{
				SlideRepository.Instance.Delete(dbSlide);
			}
		}
	}
}