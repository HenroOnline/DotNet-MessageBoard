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

		public BoardModel() : this(null) { }
		public BoardModel(DAL.Entity.Board board)
		{
			if (board == null)
			{
				return;
			}

			Id = board.Id;
			Key = board.Key;
			Description = board.Description;
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