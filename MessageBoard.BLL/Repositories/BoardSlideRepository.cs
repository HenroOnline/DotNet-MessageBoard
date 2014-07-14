using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class BoardSlideRepository : BaseRepository<BoardSlide, BoardSlideRepository>
	{
		public List<BoardSlide> ListByBoard(int boardId)
		{
			return Context.BoardSlides.Include("Slide")
																.Where(bs => bs.ModifiedKind != "D")
																.Where(bs => bs.Board.Id == boardId)
																.OrderBy(bs => bs.Sequence).ToList();
		}
	}
}
