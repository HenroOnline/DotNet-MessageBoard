using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Board
{
	public class IndexViewModel : BaseViewModel
	{
		public List<BoardModel> Boards { get; set; }

		public IndexViewModel()
		{
			Menu = "Board";
			Boards = new List<BoardModel>();

			AddCrumblePath("Schermen", "~/Board");
		}

		public static IndexViewModel Create()
		{
			var result = new IndexViewModel();

			foreach (var b in BoardRepository.Instance.ListOrderedByDescription())
			{
				result.Boards.Add(new BoardModel
				{
					Id = b.Id,
					Key = b.Key,
					Description = b.Description
				});
			}
			return result;
		}
	}
}