using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models
{
	public class BaseViewModel
	{
		public string Menu { get; set; }

		public List<System.Web.UI.Pair> CrumblePath { get; set; }
		public System.Web.UI.Pair CurrentPage { get; set; }

		public void AddCrumblePath(string name, string url)
		{
			if (CurrentPage != null)
			{
				CrumblePath.Add(new System.Web.UI.Pair
					{
						First = CurrentPage.First,
						Second = CurrentPage.Second
					});
			}
			
			CurrentPage = new System.Web.UI.Pair
			{
				First = name,
				Second = url
			};
		}

		public BaseViewModel()
		{
			CrumblePath = new List<System.Web.UI.Pair>();

		}
	}
}