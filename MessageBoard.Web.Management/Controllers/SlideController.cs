using MessageBoard.Web.Management.Models.Slide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Management.Controllers
{
	public class SlideController : Controller
	{
		public ActionResult Index()
		{
			return View(IndexViewModel.Create());
		}

		public ActionResult Detail(int? id)
		{
			id = id.HasValue ? id.Value : 0;

			return View(DetailViewModel.Create(id.Value));
		}

		[HttpPost]
		public ActionResult Detail(DetailViewModel model, string action)
		{
			switch (action)
			{
				case "save":
					if (ModelState.IsValid)
					{
						var isNew = model.Slide.Id == 0;
						model.Save();

						if (isNew)
						{
							return RedirectToAction("Detail", new { id = model.Slide.Id });
						}
						return RedirectToAction("Index");
					}
					break;
				case "delete":
					model.Delete();
					return RedirectToAction("Index");
			}

			return View(model);
		}

		public ActionResult LayerDetail(int? slideId, int? id)
		{
			id = id.HasValue ? id.Value : 0;
			slideId = slideId.HasValue ? slideId.Value : 0;

			return View(LayerDetailViewModel.Create(id.Value, slideId.Value));
		}

		[HttpPost]
		public ActionResult LayerDetail(LayerDetailViewModel model, string action)
		{
			switch (action)
			{
				case "save":
					if (ModelState.IsValid)
					{
						var isNew = model.Layer.Id == 0;
						model.Save();

						if (isNew)
						{
							return RedirectToAction("LayerDetail", new { id = model.Layer.Id });
						}
						return RedirectToAction("Detail", new { id = model.Layer.SlideId });
					}
					break;
				case "delete":
					model.Delete();
					return RedirectToAction("Detail", new { id = model.Layer.SlideId });
			}

			return View(model);
		}

		public ActionResult MessageDetail(int? layerId, int? id)
		{
			id = id.HasValue ? id.Value : 0;
			layerId = layerId.HasValue ? layerId.Value : 0;

			return View(MessageDetailViewModel.Create(id.Value, layerId.Value));
		}

		[HttpPost]
		public ActionResult MessageDetail(MessageDetailViewModel model, string action)
		{
			switch (action)
			{
				case "save":
					if (ModelState.IsValid)
					{
						var isNew = model.Message.Id == 0;
						model.Save();

						if (isNew)
						{
							return RedirectToAction("MessageDetail", new { id = model.Message.Id });
						}
						return RedirectToAction("LayerDetail", new { id = model.Message.LayerId});
					}
					break;
				case "delete":
					model.Delete();
					return RedirectToAction("LayerDetail", new { id = model.Message.LayerId });
			}

			return View(model);
		}
	}
}