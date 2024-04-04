using EmployeesApp.Models;
using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EmployeesApp.Controllers
{
    public class CompanyController(DataAccess dataAccess, ErrorLog errorLog) : Controller
    {
		private readonly DataAccess _dataAccess = dataAccess;
		private readonly ErrorLog _errorLog = errorLog;

		[HttpGet]
        public IActionResult Index()
        {
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CompanyModel company)
		{
			string filePath = "";
			try
			{
				var file = HttpContext.Request.Form.Files.FirstOrDefault();
				if (file is not null)
				{
					filePath = await _dataAccess.LoadFile(file);

					await _dataAccess.CompaniesLoadFromFile(filePath);
				}
				else
				{
					var validationResults = new List<ValidationResult>();
					var isValid = Validator.TryValidateObject(company, new(company), validationResults, true);

					if (!isValid)
					{
						return Json(new { ok = false, validation = validationResults.Select(vr => vr.ErrorMessage) });
					}

					await _dataAccess.CompaniesAdd(company);
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Компания с таким ИНН уже существует в базе." } });
				}
				else
				{
					return Json(new { ok = false, validation = new string[] { "Некорректные данные." } });
				}
			}
			catch (FileLoadException)
			{
				return Json(new { ok = false, validation = new string[] { "Некорректный формат файла." } });
			}
			catch (Exception ex)
			{
				await _errorLog.LogError("EmployeeController -> Create", JsonSerializer.Serialize(company), ex.Message);
				return Json(new { ok = false });
			}
			finally
			{
				if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
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
				return Json(new { ok = false });
			}
		}
	}
}
