using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class InformationTextConfiguration : BaseConfiguration<InformationText>
	{
		public InformationTextConfiguration()
		{
			if (!string.IsNullOrEmpty(MessageBoardContext.SchemaName))
			{
				this.ToTable("InformationText", MessageBoardContext.SchemaName);
			}

			this.Property(it => it.Title).HasMaxLength(150)
																	 .IsRequired();

			this.Property(it => it.Text).IsRequired();
		}
	}
}
