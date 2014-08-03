using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class SettingConfiguration : BaseConfiguration<Setting>
	{
		public SettingConfiguration()
		{
			this.ToTable("Setting", MessageBoardContext.SchemaName);
			
			this.Property(s => s.Key).HasMaxLength(250)
															 .IsRequired();
		}
	}
}
