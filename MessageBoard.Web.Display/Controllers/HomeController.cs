using MessageBoard.BLL.Repositories;
using MessageBoard.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var a = BoardRepository.Instance.List();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}