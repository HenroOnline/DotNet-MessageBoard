using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Display.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Display.Models.Board
{
	public class IndexViewModel
	{
		public int BoardId { get; set; }

		public List<SlideModel> Slides { get; set; }

		public int RefreshPageInterval { get; set; }

		public string ManagementBaseUrl { get; set; }

		public string DisplayBaseUrl { get; set; }

		public MvcHtmlString GlobalScripts { get; set; }

		public MvcHtmlString InstanceScripts { get; set; }
		
		public static IndexViewModel Create(string key, string path)
		{
			var board = BoardRepository.Instance.SelectByKey(key);
			if (board == null)
			{
				return null;
			}

			var result = new IndexViewModel();
			result.BoardId = board.Id;
			result.Slides = new List<SlideModel>();
			foreach (var bs in BoardSlideRepository.Instance.ListByBoard(board.Id))
			{
				result.Slides.Add(SlideModel.Create(bs));
			}

			if (result.Slides.Any())
			{
				result.Slides[0].FirstSlide = true;
			}

			var rawRefreshPageInterval = ConfigurationManager.AppSettings["RefreshPageInterval"];
			int refreshPageInterval;
			int.TryParse(rawRefreshPageInterval, out refreshPageInterval);
			result.RefreshPageInterval = refreshPageInterval;
			result.ManagementBaseUrl = ConfigurationManager.AppSettings["ManagementBaseUrl"];
			result.DisplayBaseUrl = path;


			// Retrieve all scripts
			var instanceScripts = new StringBuilder();
			var globalScripts = new Dictionary<string, string>();
			foreach (var m in result.Slides.SelectMany(s => s.Layers.SelectMany(l => l.Messages)))
			{
				instanceScripts.Append(m.InstanceScript);

				if (!string.IsNullOrWhiteSpace(m.GlobalScript) && !globalScripts.ContainsKey(m.Key))
				{
					globalScripts.Add(m.Key, m.GlobalScript);
				}
			}

			result.InstanceScripts = new MvcHtmlString(instanceScripts.ToString());

			if (globalScripts.Values.Any())
			{
				result.GlobalScripts = new MvcHtmlString(globalScripts.Values.Aggregate((s1, s2) => s1 + s2));
			}
			
			return result;
		}
	}
}