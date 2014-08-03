using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MessageBoard.Web.Display.App_Start
{
	public class BundleLogic
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			var scriptBundle = new ScriptBundle("~/Scripts");
			scriptBundle.Include("~/Scripts/jquery-2.1.1.min.js");
			bundles.Add(scriptBundle);

			var styleBundle = new StyleBundle("~/Content/css");
			styleBundle.Include("~/Content/bootstrap.min.css");
			styleBundle.Include("~/Content/bootstrap-theme.min.css");
			styleBundle.Include("~/Content/site.css");

			bundles.Add(styleBundle);
		}
	}
}