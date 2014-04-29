using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Entity
{
	public class Base
	{
		public int Id
		{
			get;
			set;
		}

		public DateTime CreatedDate
		{
			get;
			set;
		}

		public string CreatedUser
		{
			get;
			set;
		}

		public string ModifiedKind
		{
			get;
			set;
		}

		public DateTime ModifiedDate
		{
			get;
			set;
		}

		public string ModifiedUser
		{
			get;
			set;
		}
	}
}
