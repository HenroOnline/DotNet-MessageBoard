using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class LayerConfiguration : BaseConfiguration<Layer>
	{
		public LayerConfiguration()
		{
			if (!string.IsNullOrEmpty(MessageBoardContext.SchemaName))
			{
				this.ToTable("Layer", MessageBoardContext.SchemaName);
			}

			this.Property(s => s.Description).HasMaxLength(150)
																			 .IsRequired();

			this.Property(s => s.Columns).IsRequired();
			this.Property(s => s.Rows).IsRequired();
			this.Property(s => s.Sequence).IsRequired();
		}
	}
}
