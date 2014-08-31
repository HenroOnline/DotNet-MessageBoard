using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Home
{
	public class IndexViewModel : BaseViewModel
	{
		public int SelectedBoardId { get; set; }

		public List<BoardModel> Boards { get; set; }

		public static IndexViewModel Create()
		{
			var result = new IndexViewModel();

			result.Boards = new List<BoardModel>();
			foreach (var b in BoardRepository.Instance.ListOrderedByDescription())
			{
				result.Boards.Add(BoardModel.Create(b));
			}
			return result;
		}
	}
}