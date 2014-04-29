using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class Message : Base
	{
		public string Description { get; set; }

		public int PositionX { get; set; }

		public int PositionY { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public List<Setting> Settings { get; set; }
	}
}
