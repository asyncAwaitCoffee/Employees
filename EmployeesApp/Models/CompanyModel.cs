using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Models
{
	public class CompanyModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Наименование не может быть пустым.")]
        [StringLength(50, ErrorMessage = "Наименование может содержать до 50 символов.")]
        public string Name { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "ИНН не может быть пустым.")]
		[RegularExpression(@"\d{10}", ErrorMessage = "ИНН должен состоять из 10 цифр.")]
		public string Inn { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Юридический адрес не может быть пустым.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Юридический адрес может содержать до 50 символов.")]
		public string Legal { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Фактический адрес не может быть пустым.")]
		[StringLength(50, MinimumLength = 1, ErrorMessage = "Фактический адрес может содержать до 50 символов.")]
		public string Fact { get; set; }
        public int CompanyId { get; set; }

		internal void Deconstruct(out string name, out string inn, out string legal, out string fact)
		{
			name = Name;
			inn = Inn;
			legal = Legal;
			fact = Fact;
		}
	}
}
