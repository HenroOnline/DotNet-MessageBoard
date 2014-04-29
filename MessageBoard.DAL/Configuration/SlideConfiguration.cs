using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class SlideConfiguration : BaseConfiguration<Slide>
	{
		public SlideConfiguration()
		{
			if (!string.IsNullOrEmpty(MessageBoardContext.SchemaName))
			{
				this.ToTable("Slide", MessageBoardContext.SchemaName);
			}

			this.Property(s => s.Description).HasMaxLength(150)
																			 .IsRequired();

			this.Property(s => s.Columns).IsRequired();
			this.Property(s => s.Rows).IsRequired();
		}
	}
}
