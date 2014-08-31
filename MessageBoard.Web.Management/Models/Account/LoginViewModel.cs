using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Account
{
	public class LoginViewModel : BaseViewModel
	{
		[Required(ErrorMessage = "{0} is verplicht")]
		[Display(Name = "Gebruikersnaam")]
		public string Username { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[DataType(DataType.Password)]
		[Display(Name = "Wachtwoord")]
		public string Password { get; set; }

		public static LoginViewModel Create()
		{
			var result = new LoginViewModel();

			result.AddCrumblePath("Login", "~/Account/Login");

			return result;
		}
	}
}