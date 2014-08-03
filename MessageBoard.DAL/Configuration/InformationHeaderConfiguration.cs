using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class InformationHeaderConfiguration : BaseConfiguration<InformationHeader>
	{
		public InformationHeaderConfiguration()
		{
			this.ToTable("InformationHeader", MessageBoardContext.SchemaName);

			this.Property(ih => ih.Name).IsRequired()
																	.HasMaxLength(150);

			this.Property(ih => ih.InformationKind).HasMaxLength(150)
																						 .IsRequired();
		}
	}
}
