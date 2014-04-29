using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class MessageSetting : Base
	{
		public Message Message { get; set; }

		public Setting Setting { get; set; }
	}
}
