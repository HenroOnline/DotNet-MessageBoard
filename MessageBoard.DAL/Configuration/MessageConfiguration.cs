using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class MessageConfiguration : BaseConfiguration<Message>
	{
		public MessageConfiguration()
		{
			this.ToTable("Message", MessageBoardContext.SchemaName);
			
			this.Property(m => m.Description).HasMaxLength(150)
																			 .IsRequired();

			this.Property(m => m.PositionX).IsRequired();
			this.Property(m => m.PositionY).IsRequired();
			this.Property(m => m.Width).IsRequired();
			this.Property(m => m.Height).IsRequired();
			this.Property(m => m.MessageKind).HasMaxLength(150)
																			 .IsRequired();
		}
	}
}
