using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class InformationDataRepository : BaseRepository<InformationData, InformationDataRepository>, MessageBoard.Core.InformationKind.IInformationRepository
	{
		public List<InformationData> ListByHeader(int informationHeaderId)
		{
			return Context.InformationData.Where(id => id.ModifiedKind != "D")
																		.Where(id => id.InformationHeaderId == informationHeaderId)
																		.ToList();
		}

		public MessageBoard.Core.InformationKind.InformationDataList RetrieveData(int informationHeaderId)
		{
			var result = new MessageBoard.Core.InformationKind.InformationDataList();

			var data = ListByHeader(informationHeaderId);
			foreach (var dataRecord in data)
			{
				result.Add(new MessageBoard.Core.InformationKind.InformationData
					{
						Column = dataRecord.Column,
						Row = dataRecord.Row,
						Content = dataRecord.Content
					});
			}

			return result;
		}

		public MessageBoard.Core.InformationKind.InformationKind RetrieveInformationKind(int informationHeaderId)
		{
			var informationHeader = InformationHeaderRepository.Instance.Select(informationHeaderId);
			if (informationHeader == null)
			{
				return null;
			}

			return MessageBoard.Core.InformationKind.InformationKind.Select(informationHeader.InformationKind);
		}
	}
}
