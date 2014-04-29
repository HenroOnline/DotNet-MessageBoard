using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class Slide : Base
	{
		public string Description { get; set; }

		public int Columns { get; set; }

		public int Rows { get; set; }

		public ICollection<Message> Messages { get; set; }
	}
}
