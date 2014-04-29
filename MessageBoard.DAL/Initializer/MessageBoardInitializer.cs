using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Initializer
{
	public class MessageBoardInitializer : CreateDatabaseIfNotExists<MessageBoardContext>
	{
		protected override void Seed(MessageBoardContext context)
		{
			var board = new Board();
			board.Key = "Main";
			board.Description = "Main screen";

			var slides = new List<Slide>();
			slides.Add(new Slide
				{
					Description = "Slide 1",
					Columns = 16,
					Rows = 9
				});

			slides.Add(new Slide
			{
				Description = "Slide 2",
				Columns = 16,
				Rows = 9
			});

			context.Boards.Add(board);
			context.Slides.AddRange(slides);

			int sequence = 1;
			slides.ForEach(s => context.BoardSlides.Add(new BoardSlide
				{
					Board = board,
					Slide = s,
					Sequence = sequence++
				}));
		}
	}
}
