using MessageBoard.Web.Management.Models;
using MessageBoard.Web.Management.Models.Account;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MessageBoard.Web.Management.Controllers
{
	public class AccountController : Controller
	{
		protected string Username
		{
			get
			{
				return ConfigurationManager.AppSettings["Account.Username"];
			}
		}

		protected string Password
		{
			get
			{
				return ConfigurationManager.AppSettings["Account.Password"];
			}
		}

		public ActionResult Login(string returnUrl)
		{
			if (string.IsNullOrEmpty(Username))
			{
				return HandleSuccessFulLogon(returnUrl);
			}
			return View(LoginViewModel.Create());
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (model.Username == Username
						&& model.Password == Password)
				{
					return HandleSuccessFulLogon(returnUrl);
				}
				else
				{
					ModelState.AddModelError("UnknownUser", "Combinatie gebruikersnaam wachtwoord onjuist");
				}
			}

			return View(LoginViewModel.Create());
		}

		public ActionResult SignOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}

		protected ActionResult HandleSuccessFulLogon(string returnUrl)
		{
			FormsAuthentication.SetAuthCookie(Username, false);
			if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
										&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
	}
}