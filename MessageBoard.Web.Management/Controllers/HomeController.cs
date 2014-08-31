using MessageBoard.Web.Management.Hubs;
using MessageBoard.Web.Management.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Management.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{			
			return View(IndexViewModel.Create());
		}

		[HttpPost]
		public ActionResult Index(IndexViewModel model)
		{
			ContentBroadCaster.Instance.UpdateContent(model.SelectedBoardId);

			return View(IndexViewModel.Create());
		}
	}
}