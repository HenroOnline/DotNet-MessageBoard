using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class Setting : Base
	{
		public string Key { get; set; }

		public string StringValue { get; set; }
	}
}
