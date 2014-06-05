using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class BoardConfiguration : BaseConfiguration<Board>
	{
		public BoardConfiguration()
		{
			if (!string.IsNullOrEmpty(MessageBoardContext.SchemaName))
			{
				this.ToTable("Board", MessageBoardContext.SchemaName);
			}

			this.Property(b => b.Key).HasMaxLength(150)
															 .IsRequired();

			this.Property(b => b.Description).HasMaxLength(150)
																			 .IsRequired();			
		}
	}
}
