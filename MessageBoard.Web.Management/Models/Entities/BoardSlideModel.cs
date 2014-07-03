﻿using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class BoardSlideModel
	{
		public int Id { get; set; }
		public int SlideId { get; set; }

		public static BoardSlideModel Create (BoardSlide boardSlide)
		{
			var result = new BoardSlideModel();
			if (boardSlide == null)
			{
				return result;
			}

			result.Id = boardSlide.Id;
			result.SlideId = boardSlide.SlideId;

			return result;
		}		
	}
}