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
		public List<BoardSlideModel> LinkedSlides { get; set; }
		public List<SlideModel> AvailableSlides { get; set; }

		public DetailViewModel()
		{
			Menu = "Board";
		}

		public static DetailViewModel Create(int id)
		{
			var result = new DetailViewModel();

			result.Board = BoardModel.Create(BoardRepository.Instance.Select(id));
			
			result.AvailableSlides = new List<SlideModel>();
			foreach (var slide in SlideRepository.Instance.ListOrderedByDescription())
			{
				result.AvailableSlides.Add(SlideModel.Create(slide));
			}

			result.LinkedSlides = new List<BoardSlideModel>();
			if (result.Board.Id != 0)
			{
				foreach (var bs in BoardSlideRepository.Instance.ListByBoard(result.Board.Id))
				{					
					result.LinkedSlides.Add(BoardSlideModel.Create(bs));
				}
			}

			result.AddCrumblePath("Schermen", "~/Board");
			result.AddCrumblePath((result.Board.Id == 0) ? "Scherm toevoegen" : result.Board.Description, string.Format("~/Board/", result.Board.Id));
			
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

			var currentBoardSlides = BoardSlideRepository.Instance.ListByBoard(dbBoard.Id);
			if (LinkedSlides != null)
			{
				var currentSequence = 10;
				foreach (var newSlide in LinkedSlides)
				{
					DAL.Entity.BoardSlide dbBoardSlide = null;
					if (newSlide.Id != 0)
					{
						dbBoardSlide = BoardSlideRepository.Instance.Select(newSlide.Id);

						if (dbBoardSlide != null)
						{
							currentBoardSlides.RemoveAll(bs => bs.Id == dbBoardSlide.Id);
						}
					}

					if (dbBoardSlide == null)
					{
						dbBoardSlide = BoardSlideRepository.Instance.NewEntity();
						dbBoardSlide.BoardId = dbBoard.Id;
					}
					dbBoardSlide.SlideId = newSlide.SlideId;
					dbBoardSlide.Duration = newSlide.Duration;
					dbBoardSlide.Sequence = currentSequence;

					BoardSlideRepository.Instance.Save(dbBoardSlide);

					currentSequence += 10;
				}
			}

			foreach (var bs in currentBoardSlides)
			{
				BoardSlideRepository.Instance.Delete(bs);
			}			
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