using EmployeesApp.Models;
using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Controllers
{
	public class EmployeeController(DataAccess dataAccess) : Controller
	{
		private readonly DataAccess _dataAccess = dataAccess;

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			try
			{
				var companies = await _dataAccess.CompaniesGetAll();
				return View(companies);
			}
			catch (Exception)
			{
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

					return Json(new { ok = true });
				}
			}
			catch(SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Сотрудник с указанной серией и номером паспорта уже существует в базе." } });
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
			finally
			{
				if (!filePath.IsNullOrEmpty() && System.IO.File.Exists(filePath))
				{
					System.IO.File.Delete(filePath);
				}
			}

			var validationResults = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(employee, new(employee), validationResults, true);

			if (!isValid)
			{
				return Json(new { ok = false, validation = validationResults.Select(vr => vr.ErrorMessage) });
			}

			try
			{
				await _dataAccess.EmployeesAdd(employee);
			}
			catch (SqlException ex)
			{
				if (ex.Number == 2627)
				{
					return Json(new { ok = false, validation = new string[] { "Сотрудник с указанной серией и номером паспорта уже существует в базе." } });
				}
				throw;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Json(new { ok = false });
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
				Console.WriteLine(ex.Message);
				return Json(new { ok = false });
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetById(int employeeId)
		{
			Console.WriteLine(employeeId);
			try
			{
				var employee = await _dataAccess.EmployeesGetById(employeeId);
				return Json(new { ok = true, data = employee });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Json(new { ok = false });
			}
		}
	}
}