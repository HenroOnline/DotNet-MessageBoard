using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class InformationDataRepository : BaseRepository<InformationData, InformationDataRepository>
	{
		public List<InformationData> ListByHeader(int headerId)
		{
			return Context.InformationData.Where(id => id.ModifiedKind != "D")
																		.Where(id => id.InformationHeaderId == headerId)
																		.ToList();
		}
	}
}
