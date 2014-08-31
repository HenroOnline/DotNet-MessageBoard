using MessageBoard.Web.Display.Models.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Controllers
{
	public class BoardController : Controller
	{
		public ActionResult Index(string key)
		{
			var model = IndexViewModel.Create(key, System.Web.HttpContext.Current.Request.Url.AbsolutePath);
			if (model == null)
			{
				return RedirectToAction("NoData");
			}
			
			return View(model);
		}

		public ActionResult NoData()
		{
			return View();
		}
	}
}