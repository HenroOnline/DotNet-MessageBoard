using MessageBoard.BLL.Repositories;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Information
{
	public class IndexViewModel : BaseViewModel
	{
		public IndexViewModel()
		{
			Menu = "Information";
			InformationHeaders = new List<InformationHeaderModel>();

			AddCrumblePath("Informatie", "~/Information");
		}

		public List<InformationHeaderModel> InformationHeaders { get; set; }

		public static IndexViewModel Create()
		{
			var result = new IndexViewModel();
			
			foreach (var informationHeader in InformationHeaderRepository.Instance.ListOrderedByName())
			{
				result.InformationHeaders.Add(InformationHeaderModel.Create(informationHeader, false));
			}

			return result;
		}
	}
}