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
					Rows = 9,
					Messages = new List<Message>()
					{
						new Message
						{
							Description = "Bericht 1",
							PositionX = 0,
							PositionY = 0,
							Width = 4,
							Height = 5,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 2",
							PositionX = 4,
							PositionY = 0,
							Width = 4,
							Height = 6,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 3",
							PositionX = 0,
							PositionY = 5,
							Width = 4,
							Height = 1,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 4",
							PositionX = 0,
							PositionY = 6,
							Width = 16,
							Height = 1,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 5",
							PositionX = 0,
							PositionY = 7,
							Width = 16,
							Height = 1,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 6",
							PositionX = 0,
							PositionY = 8,
							Width = 8,
							Height = 1,
							MessageKind = "InformationText"
						},

						new Message
						{
							Description = "Bericht 7",
							PositionX = 8,
							PositionY = 8,
							Width = 8,
							Height = 1,
							MessageKind = "InformationText"
						},
						new Message
						{
							Description = "Bericht 10",
							PositionX = 12,
							PositionY = 3,
							Width = 4,
							Height = 3,
							MessageKind = "InformationText"
						},
						new Message
						{
							Description = "Bericht 8",
							PositionX = 8,
							PositionY = 0,
							Width = 8,
							Height = 3,
							MessageKind = "InformationText"
						},
						new Message
						{
							Description = "Bericht 9",
							PositionX = 8,
							PositionY = 3,
							Width = 4,
							Height = 3,
							MessageKind = "InformationText"
						}						
					}
				});

			slides.Add(new Slide
			{
				Description = "Slide 2",
				Columns = 16,
				Rows = 9,
				Messages = new List<Message>()
				{
					new Message
					{
						Description = "Bericht 1",
						PositionX = 0,
						PositionY = 0,
						Width = 4,
						Height = 5,
						MessageKind = "InformationText"
					},

					new Message
					{
						Description = "Bericht 2",
						PositionX = 4,
						PositionY = 0,
						Width = 4,
						Height = 6,
						MessageKind = "InformationText"
					}
				}
			});

			context.Boards.Add(board);
			context.Slides.AddRange(slides);

			int sequence = 1;
			slides.ForEach(s => context.BoardSlides.Add(new BoardSlide
				{
					Board = board,
					Slide = s,
					Sequence = sequence++,
				}));
		}
	}
}
