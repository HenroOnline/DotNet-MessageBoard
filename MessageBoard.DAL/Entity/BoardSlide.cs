using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class BoardSlide : Base
	{
		public int BoardId { get; set; }
		public Board Board { get; set; }

		public int SlideId { get; set; }
		public Slide Slide { get; set; }

		public int Sequence { get; set; }
	}
}
