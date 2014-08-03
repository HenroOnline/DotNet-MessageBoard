using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class InformationDataConfiguration : BaseConfiguration<InformationData>
	{
		public InformationDataConfiguration()
		{
			this.ToTable("InformationData", MessageBoardContext.SchemaName);

			this.Property(id => id.Column).IsRequired();
			this.Property(id => id.Row).IsRequired();
			this.Property(id => id.Content).IsRequired();
		}
	}
}
