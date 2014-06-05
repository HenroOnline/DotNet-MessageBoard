using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class BoardRepository : BaseRepository<Board, BoardRepository>
	{
		public Board SelectByKey(string key)
		{
			return Context.Boards.FirstOrDefault(b => b.ModifiedKind != "D" && b.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
		}

		public List<Board> ListOrderedByDescription()
		{
			return Context.Boards.Where(b => b.ModifiedKind != "D")
													 .OrderBy(b => b.Description).ToList();
		}
	}
}
