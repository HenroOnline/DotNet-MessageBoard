using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core
{
	public class MessageKindSettingList : List<MessageKindSetting>
	{
		public MessageKindSetting this[string key]
		{
			get
			{
				return this.FirstOrDefault(m => m.Key == key);
			}
		}
	}
}
