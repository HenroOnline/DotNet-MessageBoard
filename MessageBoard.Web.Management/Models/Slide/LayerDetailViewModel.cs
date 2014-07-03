using MessageBoard.BLL.Repositories;
using MessageBoard.DAL.Entity;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Slide
{
	public class LayerDetailViewModel : BaseViewModel
	{
		public LayerModel Layer { get; set; }

		public List<MessageModel> Messages { get; set; }

		public LayerDetailViewModel()
		{
			Menu = "Slide";
		}

		public static LayerDetailViewModel Create(int id, int slideId)
		{
			var result = new LayerDetailViewModel();

			var dbLayer = LayerRepository.Instance.Select(id);

			DAL.Entity.Slide dbSlide = null;
			if (slideId != 0)
			{
				dbSlide = SlideRepository.Instance.Select(slideId);
			}
			else
			{
				dbSlide = SlideRepository.Instance.Select(dbLayer.SlideId);
			}

			result.Layer = LayerModel.Create(dbLayer, dbSlide);

			result.AddCrumblePath("Slides", "~/Slide");
			result.AddCrumblePath(dbSlide.Description, string.Concat("~/Slide/Detail/", dbSlide.Id));
			result.AddCrumblePath((result.Layer.Id == 0) ? "Layer toevoegen" : result.Layer.Description, string.Format("~/Slide/LayerDetail/?slideId={0}&id={1}", dbSlide.Id, result.Layer.Id));

			result.Messages = new List<MessageModel>();

			if (result.Layer.Id != 0)
			{
				foreach (var message in MessageRepository.Instance.ListByLayer(result.Layer.Id))
				{
					result.Messages.Add(MessageModel.Create(message, dbLayer));
				}
			}

			return result;
		}

		public void Save()
		{
			DAL.Entity.Layer dbLayer = null;
			if (Layer.Id != 0)
			{
				dbLayer = LayerRepository.Instance.Select(Layer.Id);
			}

			if (dbLayer == null)
			{
				dbLayer = new DAL.Entity.Layer();
				dbLayer.SlideId = Layer.SlideId;
			}

			dbLayer.Description = Layer.Description;
			dbLayer.Columns = Layer.Columns;
			dbLayer.Rows = Layer.Rows;
			dbLayer.Sequence = Layer.Sequence;

			LayerRepository.Instance.Save(dbLayer);

			Layer.Id = dbLayer.Id;
		}

		public void Delete()
		{
			var dbLayer = LayerRepository.Instance.Select(Layer.Id);
			if (dbLayer != null)
			{
				LayerRepository.Instance.Delete(dbLayer);
			}
		}
	}
}