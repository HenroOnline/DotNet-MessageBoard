using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class Layer : Base
	{
		public string Description { get; set; }

		public int Columns { get; set; }

		public int Rows { get; set; }

		public int Sequence { get; set; }

		public int SlideId { get; set; }
		public Slide Slide { get; set; }

		public ICollection<Message> Messages { get; set; }
	}
}
