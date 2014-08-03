using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class InformationHeaderRepository : BaseRepository<InformationHeader, InformationHeaderRepository>
	{
		public List<InformationHeader> ListOrderedByName()
		{
			return Context.InformationHeaders.Where(ih => ih.ModifiedKind != "D")
																			 .OrderBy(ih => ih.Name)
																			 .ToList();
		}

		public override void Delete(InformationHeader entity)
		{
			foreach (var dataRecord in InformationDataRepository.Instance.ListByHeader(entity.Id))
			{
				InformationDataRepository.Instance.Delete(dataRecord);
			}
			base.Delete(entity);
		}
	}
}
