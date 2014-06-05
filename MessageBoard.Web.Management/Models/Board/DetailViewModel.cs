using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Board
{
	public class DetailViewModel : BaseViewModel
	{
		public BoardModel Board { get; set; }
		public DetailViewModel()
		{

		}

		public static DetailViewModel Create(int id)
		{
			var result = new DetailViewModel();

			result.Board = new BoardModel(BoardRepository.Instance.Select(id));
			
			return result;
		}

		public void Save()
		{
			DAL.Entity.Board dbBoard = null;
			if (Board.Id != 0)
			{
				dbBoard = BoardRepository.Instance.Select(Board.Id);
			}

			if (dbBoard == null)
			{
				dbBoard = new DAL.Entity.Board();
			}

			dbBoard.Key = Board.Key;
			dbBoard.Description = Board.Description;
			
			BoardRepository.Instance.Save(dbBoard);

			Board.Id = dbBoard.Id;
		}

		public void Delete()
		{
			var dbBoard = BoardRepository.Instance.Select(Board.Id);
			if (dbBoard != null)
			{
				BoardRepository.Instance.Delete(dbBoard);
			}
		}
	}
}