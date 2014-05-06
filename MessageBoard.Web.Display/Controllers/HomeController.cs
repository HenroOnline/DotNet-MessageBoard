using MessageBoard.BLL.Repositories;
using MessageBoard.DAL;
using MessageBoard.Web.Display.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string key)
		{
			key = "Main";
			var board = BoardRepository.Instance.SelectByKey(key);
			var slide = SlideRepository.Instance.ListByBoard(board.Id).FirstOrDefault();
			var messages = MessageRepository.Instance.ListBySlide(slide.Id);

			return View(new SlideModel(slide, messages));
		}
	}
}