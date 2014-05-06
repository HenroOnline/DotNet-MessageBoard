using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class MessageRepository : BaseRepository<Message, MessageRepository>
	{
		public List<Message> ListBySlide(int slideId)
		{
			return Context.Messages.Where(m => m.Slide.Id == slideId).ToList();
		}
	}
}
