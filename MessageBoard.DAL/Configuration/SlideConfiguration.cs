﻿using MessageBoard.DAL.Entity;
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
			this.ToTable("Slide", MessageBoardContext.SchemaName);
			
			this.Property(s => s.Description).HasMaxLength(150)
																			 .IsRequired();
		}
	}
}
