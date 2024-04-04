using EmployeesApp.Models;
using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EmployeesApp.Controllers
{
	public class EmployeeController(DataAccess dataAccess, ErrorLog errorLog) : Controller
	{
		private readonly DataAccess _dataAccess = dataAccess;
		private readonly ErrorLog _errorLog = errorLog;

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			try
			{
				var companies = await _dataAccess.CompaniesGetAll();
				return View(companies);
			}
			catch (Exception ex)
			{
				await _errorLog.LogError("EmployeeController -> Index", "", ex.Message);
				return BadRequest();
			}
		}
		[HttpPost]
		public async Task<IActionResult> Create(EmployeeModel employee)
		{
			string filePath = "";
			try
			{
				var file = HttpContext.Request.Form.Files.FirstOrDefault();
				if (file is not null)
				{
					filePath = await _dataAccess.LoadFile(file);

					await _dataAccess.EmployeesLoadFromFile(filePath);
				}
				else
				{
					var validationResults = new List<ValidationResult>();
					var isValid = Validator.TryValidateObject(employee, new(employee), validationResults, true);

					if (!isValid)
					{
						return Json(new { ok = false, validation = validationResults.Select(vr => vr.ErrorMessage) });
					}

					await _dataAccess.EmployeesAdd(employee);
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Сотрудник с указанной серией и номером паспорта уже существует в базе." } });
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
				await _errorLog.LogError("EmployeeController -> Create", JsonSerializer.Serialize(employee), ex.Message);
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
		public async Task<IActionResult> ByCompany(int companyId)
		{
			try
			{
				var employeesByCompany = await _dataAccess.EmployeesGetByCompany(companyId);
                return Json(new { ok = true, data = employeesByCompany });
			}
			catch (Exception ex)
			{
				await _errorLog.LogError("EmployeeController -> ByCompany", $"companyId = { companyId }", ex.Message);
				return Json(new { ok = false, validation = new string[] { "Ошибка на сервере. Обратитесь к администрации сайта." } });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetById(int employeeId)
		{
			try
			{
				var employee = await _dataAccess.EmployeesGetById(employeeId);
				return Json(new { ok = true, data = employee });
			}
			catch (Exception ex)
			{
				await _errorLog.LogError("EmployeeController -> GetById", $"companyId = { employeeId }", ex.Message);
				return Json(new { ok = false, validation = new string[] { "Ошибка на сервере. Обратитесь к администрации сайта." } });
			}
		}
	}
}