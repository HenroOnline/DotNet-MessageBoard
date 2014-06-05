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
			Title = "Schermen";
			Intro = "Hier kunt u nieuwe schermen toevoegen of bestaande schermen beheren";
			Boards = new List<BoardModel>();
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