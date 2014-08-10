using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public interface IInformationRepository
	{
		InformationKind RetrieveInformationKind(int informationHeaderId);
		InformationDataList RetrieveData(int informationHeaderId);
	}
}
