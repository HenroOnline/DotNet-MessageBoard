using MessageBoard.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageBoard.Web.Management.Models.Entities
{
	public class BoardModel : IValidatableObject
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Sleutel")]		
		public string Key { get; set; }

		[Required(ErrorMessage = "{0} is verplicht")]
		[MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn")]
		[Display(Name = "Omschrijving")]
		public string Description { get; set; }

		public static BoardModel Create(DAL.Entity.Board board)
		{
			var result = new BoardModel();
			if (board == null)
			{
				return result;
			}

			result.Id = board.Id;
			result.Key = board.Key;
			result.Description = board.Description;

			return result;
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var dbBoard = BoardRepository.Instance.SelectByKey(Key);
			var result = new List<ValidationResult>();

			if (dbBoard != null && dbBoard.Id != Id)
			{
				result.Add(new ValidationResult("Sleutel is al in gebruik!", new List<string> { "Key" } ));
			}

			return result;			
		}
	}
}