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
		public List<Message> ListByLayer(int layerId)
		{
			return Context.Messages.Where(m => m.ModifiedKind != "D" && m.Layer.Id == layerId)
														 .ToList();
		}

		public override void Delete(Message entity)
		{
			foreach (var setting in SettingRepository.Instance.List(entity.Id))
			{
				SettingRepository.Instance.Delete(setting);
			}

			base.Delete(entity);
		}
	}
}
