using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class InformationHeader : Base
	{
		public string Name { get; set; }

		public string InformationKind { get; set; }

		public List<InformationData> Data { get; set; }
	}
}
