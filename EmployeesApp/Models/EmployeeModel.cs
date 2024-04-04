using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Models
{
	public class EmployeeModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Имя не может быть пустым.")]
		[StringLength(50, ErrorMessage = "Имя может содержать до 50 символов.")]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия не может быть пустой.")]
		[StringLength(50, ErrorMessage = "Фамилия может содержать до 50 символов.")]
		public string Surname { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Отчество не может быть пустым.")]
		[StringLength(50, ErrorMessage = "Отчество может содержать до 50 символов.")]
		public string Patronymic { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Дата рождения не может быть пустой.")]
		public DateOnly Birthdate { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Серия паспорта не может быть пустой.")]
		[RegularExpression(@"\d{4}", ErrorMessage = "Серия паспорта должна состоять из 4 цифр.")]
		public string Series { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Номер паспорта не может быть пустым.")]
		[RegularExpression(@"\d{6}", ErrorMessage = "Номер паспорта должен состоять из 6 цифр.")]
		public string Number { get; set; }
		public int? CompanyId { get; set; }
        public int EmployeeId { get; set; }

		internal void Deconstruct(out string name, out string surname, out string patronymic, out DateOnly birthdate, out string passportSeries, out string passportNumber, out int? companyId)
		{
			name = FirstName;
			surname = Surname;
			patronymic = Patronymic;
			birthdate = Birthdate;
			passportSeries = Series;
			passportNumber = Number;
			companyId = CompanyId;
		}
	}
}
