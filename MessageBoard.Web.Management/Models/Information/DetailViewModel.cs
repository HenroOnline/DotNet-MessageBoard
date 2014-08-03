using MessageBoard.BLL.Repositories;
using MessageBoard.Core.InformationKind;
using MessageBoard.Web.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Web.Management.Models.Information
{
	public class DetailViewModel : BaseViewModel
	{
		public InformationHeaderModel InformationHeader { get; set; }

		public List<InformationKind> InformationKindList { get; set; }

		public DetailViewModel()
		{
			Menu = "Information";
		}

		public static DetailViewModel Create(int id)
		{
			var result = new DetailViewModel();

			result.InformationHeader = InformationHeaderModel.Create(InformationHeaderRepository.Instance.Select(id), true);
			result.InformationKindList = InformationKind.List;			

			result.AddCrumblePath("Informatie", "~/Information");
			result.AddCrumblePath((result.InformationHeader.Id == 0) ? "Informatie item toevoegen" : result.InformationHeader.Name, string.Format("~/Information/", result.InformationHeader.Id));

			return result;
		}

		public void Save()
		{
			DAL.Entity.InformationHeader dbInformationHeader = null;
			if (InformationHeader.Id != 0)
			{
				dbInformationHeader = InformationHeaderRepository.Instance.Select(InformationHeader.Id);
			}

			if (dbInformationHeader == null)
			{
				dbInformationHeader = InformationHeaderRepository.Instance.NewEntity();
			}

			dbInformationHeader.Name = InformationHeader.Name;
			dbInformationHeader.InformationKind = InformationHeader.InformationKindKey;

			InformationHeaderRepository.Instance.Save(dbInformationHeader);
			InformationHeader.Id = dbInformationHeader.Id;

			var currentData = InformationDataRepository.Instance.ListByHeader(dbInformationHeader.Id);
			foreach (var newData in InformationHeader.InformationData)
			{
				if (string.IsNullOrWhiteSpace(newData.Content))
				{
					continue;
				}

				var dataRecord = currentData.FirstOrDefault(dr => dr.Row == newData.Row && dr.Column == newData.Column);
				if (dataRecord != null)
				{
					currentData.Remove(dataRecord);
				}
				else
				{
					dataRecord = InformationDataRepository.Instance.NewEntity();
					dataRecord.InformationHeaderId = dbInformationHeader.Id;
					dataRecord.Row = newData.Row;
					dataRecord.Column = newData.Column;
				}
				dataRecord.Content = newData.Content;

				InformationDataRepository.Instance.Save(dataRecord);
			}

			foreach (var dataRecord in currentData)
			{
				InformationDataRepository.Instance.Delete(dataRecord);
			}
		}

		public void Delete()
		{
			var dbInformationHeader = InformationHeaderRepository.Instance.Select(InformationHeader.Id);
			if (dbInformationHeader != null)
			{
				InformationHeaderRepository.Instance.Delete(dbInformationHeader);
			}
		}
	}
}