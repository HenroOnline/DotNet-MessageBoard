using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class SettingRepository : BaseRepository<Setting, SettingRepository>
	{
		public Setting Select(int messageId, string key)
		{
			return Context.Settings.Where(s => s.ModifiedKind != "D")
														 .Where(s => s.Key == key)
														 .Where(s => s.MessageId.HasValue && s.MessageId.Value == messageId)
														 .FirstOrDefault();
														 
		}
		
		public List<Setting> List(int messageId)
		{
			return Context.Settings.Where(s => s.ModifiedKind != "D")
														 .Where(s => s.MessageId.HasValue && s.MessageId.Value == messageId)
														 .ToList();
		}
	}
}
