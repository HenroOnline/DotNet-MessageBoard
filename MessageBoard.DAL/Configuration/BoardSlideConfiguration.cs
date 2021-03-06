﻿using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL.Configuration
{
	public class BoardSlideConfiguration : BaseConfiguration<BoardSlide>
	{
		public BoardSlideConfiguration ()
		{
			this.ToTable("BoardSlide", MessageBoardContext.SchemaName);
			
			this.HasRequired(bs => bs.Board);
			this.HasRequired(bs => bs.Slide);
			
			this.Property(bs => bs.Sequence).IsRequired();
			this.Property(bs => bs.Duration).IsRequired();		
		}
	}
}
