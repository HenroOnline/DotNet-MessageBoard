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
					Layers = new List<Layer>()
					{
						new Layer()
						{
							Columns = 1,
							Rows = 1,
							Description = "Achtergrond",
							Sequence = 10,
							Messages = new List<Message>()
							{
								new Message()
								{
									Description = "Achtergrond",
									PositionX = 0,
									PositionY = 0,
									Width = 1,
									Height = 1,
									MessageKind = "InformationText"
								}
							}
						},
						new Layer()
						{
							Columns = 16,
							Rows = 9,
							Description = "Layer 1",
							Sequence = 20,
							Messages = new List<Message>()
							{
								new Message
								{
									Description = "Bericht 1.1",
									PositionX = 0,
									PositionY = 0,
									Width = 4,
									Height = 5,
									MessageKind = "MessageBoard.Core.MessageKind.HTMLMessageKind"
								},

								new Message
								{
									Description = "Bericht 1.2",
									PositionX = 4,
									PositionY = 0,
									Width = 4,
									Height = 6,
									MessageKind = "MessageBoard.Core.MessageKind.HTMLMessageKind"
								}		
							}
						}
					}
				});


			context.Boards.Add(board);
			context.Slides.AddRange(slides);

			int sequence = 10;
			slides.ForEach(s => context.BoardSlides.Add(new BoardSlide
				{
					Board = board,
					Slide = s,
					Duration = 30,
					Sequence = (sequence += 10)
				}));
		}
	}
}
