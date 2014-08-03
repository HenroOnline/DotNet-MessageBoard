using MessageBoard.BLL.Repositories;
using MessageBoard.Core.InformationKind;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class InformationHeaderModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Naam")]		
		public string Name { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[Display(Name = "Soort")]
		public string InformationKindKey { get; set; }
		public InformationKind InformationKind { get; set; }

		public List<InformationDataModel> InformationData
		{
			get;
			set;
		}

		public static InformationHeaderModel Create(DAL.Entity.InformationHeader dbHeader, bool loadInformationData)
		{
			var result = new InformationHeaderModel();
			result.InformationData = new List<InformationDataModel>();
			if (dbHeader == null)
			{
				return result;
			}

			result.Id = dbHeader.Id;
			result.Name = dbHeader.Name;
			result.InformationKind = InformationKind.Select(dbHeader.InformationKind);
			result.InformationKindKey = result.InformationKind.Key;
			
			if (!loadInformationData || result.Id == 0)
			{
				return result;
			}
			
			foreach (var dbData in InformationDataRepository.Instance.ListByHeader(dbHeader.Id))
			{
				result.InformationData.Add(InformationDataModel.Create(dbData));
			}			

			return result;
		}
	}
}