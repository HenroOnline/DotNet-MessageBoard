using MessageBoard.Web.Management.Models;
using MessageBoard.Web.Management.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Management.Controllers
{
	public class BoardController : Controller
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
			switch(action)
			{
				case "save":
					if (ModelState.IsValid)
					{
						var isNew = model.Board.Id == 0;
						model.Save();

						if (isNew)
						{
							return RedirectToAction("Detail", new { id = model.Board.Id });
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
	}
}