using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class SlideRepository : BaseRepository<Slide, SlideRepository>
	{
		public List<Slide> ListByBoard(int boardId)
		{
			var slides = from bs in Context.BoardSlides
									 join s in Context.Slides.Include("Messages") on bs.Slide.Id equals s.Id
									 where bs.Board.Id == boardId
									 orderby bs.Sequence
									 select s;

			return slides.ToList();
		}
	}
}
