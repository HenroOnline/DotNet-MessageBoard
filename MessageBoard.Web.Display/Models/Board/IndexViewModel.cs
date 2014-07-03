﻿using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Display.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Display.Models.Board
{
	public class IndexViewModel
	{
		public List<SlideModel> Slides { get; set; }

		public static IndexViewModel Create(string key)
		{
			var board = BoardRepository.Instance.SelectByKey(key);
			if (board == null)
			{
				return null;
			}

			var result = new IndexViewModel();
			result.Slides = new List<SlideModel>();
			foreach (var slide in SlideRepository.Instance.ListByBoard(board.Id))
			{
				result.Slides.Add(SlideModel.Create(slide));
			}
			
			return result;
		}
	}
}