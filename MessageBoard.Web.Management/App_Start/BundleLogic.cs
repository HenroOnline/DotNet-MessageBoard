using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MessageBoard.Web.Management.App_Start
{
	public class BundleLogic
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			var scriptBundle = new ScriptBundle("~/Scripts");

			scriptBundle.Include("~/Scripts/Libraries/jquery-2.1.1.min.js");
			scriptBundle.Include("~/Scripts/Libraries/bootstrap.min.js");
			scriptBundle.Include("~/Scripts/Libraries/jquery.validate.min.js");
			scriptBundle.Include("~/Scripts/Libraries/jquery.validate.messages-nl.js");
			scriptBundle.Include("~/Scripts/Libraries/jquery.validate.unobtrusive.min.js");
			scriptBundle.Include("~/Scripts/Libraries/jquery.validate.unobtrusive.bootstrap.min.js");
			scriptBundle.Include("~/Scripts/Libraries/jquery-ui.min.js");			
			scriptBundle.Include("~/Scripts/Libraries/tinymce/tinymce.min.js");
			scriptBundle.Include("~/Scripts/Libraries/tinymce/tinymce.min.js");
			scriptBundle.Include("~/Scripts/Libraries/fancybox/jquery.fancybox.js");			
			scriptBundle.Include("~/Scripts/jquery.signalR-2.1.1.min.js");			
			scriptBundle.Include("~/Scripts/site.js");
			
			bundles.Add(scriptBundle);

			var styleBundle = new StyleBundle("~/Content/css");
			styleBundle.Include("~/Content/bootstrap.min.css");
			styleBundle.Include("~/Content/bootstrap-theme.min.css");
			styleBundle.Include("~/Content/jquery.fancybox.css");
			styleBundle.Include("~/Content/site.css");
			
			bundles.Add(styleBundle);
		}
	}
}