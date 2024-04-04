using EmployeesApp.Models;
using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Controllers
{
    public class CompanyController(DataAccess dataAccess) : Controller
    {
		private readonly DataAccess _dataAccess = dataAccess;

		[HttpGet]
        public IActionResult Index()
        {
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CompanyModel company)
		{
			try
			{
				var file = HttpContext.Request.Form.Files.FirstOrDefault();
				if (file is not null)
				{
					var filePath = await _dataAccess.LoadFile(file);

					await _dataAccess.CompaniesLoadFromFile(filePath);

					return Json(new { ok = true });
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Компания с таким ИНН уже существует в базе." } });
				}
				throw;
			}
			catch (FileLoadException)
			{
				return Json(new { ok = false, validation = new string[] { "Некорректный формат файла." } });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			var validationResults = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(company, new(company), validationResults, true);
			
			if (!isValid)
			{
				return Json(new { ok = false, validation = validationResults.Select(vr => vr.ErrorMessage) });
			}

			try
			{
				await _dataAccess.CompaniesAdd(company);
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Компания с таким ИНН уже существует в базе." } });
				}
				throw;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return Json(new { ok = true });
		}

		[HttpGet]
		public async Task<IActionResult> GetById(int companyId)
		{
			try
			{
				var company = await _dataAccess.CompaniesGetById(companyId);
				return Json(new { ok = true, data = company });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return BadRequest();
			}
		}
	}
}
