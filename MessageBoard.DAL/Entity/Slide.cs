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

		public ICollection<Layer> Layers { get; set; }
	}
}
