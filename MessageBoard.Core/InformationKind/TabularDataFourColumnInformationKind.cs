﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.InformationKind
{
	public class TabularDataFourColumnInformationKind : TabularDataInformationKind
	{
		public override int ColumnCount
		{
			get
			{
				return 4;
			}
		}
	}
}
