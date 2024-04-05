using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Controllers
{
	public class HomeController(DataAccess dataAccess, ErrorLog errorLog) : Controller
	{
		private readonly DataAccess _dataAccess = dataAccess;
		private readonly ErrorLog _errorLog = errorLog;

		public async Task<IActionResult> Index()
		{
			try
			{
				var companies = await _dataAccess.CompaniesGetAll();
				return View(companies);
			}
			catch (Exception ex)
			{
				await _errorLog.LogError("HomeController -> Index", "", ex.ToString());
				return BadRequest();
			}			
		}

		public async Task<IActionResult> DownloadFile(int fileId)
		{
			try
			{
				if (fileId == 1)
				{
					var filePath = @"D:\MyTemp\COMPANIES_full.csv";
					await _dataAccess.CompaniesLoadToFile();
					if (System.IO.File.Exists(filePath))
					{
						var fs = System.IO.File.OpenRead(filePath);
						return File(fs, "application/octet-stream");
					}
				}

				if (fileId == 2)
				{
					var filePath = @"D:\MyTemp\EMPLOYEES_full.csv";
					await _dataAccess.EmployeesLoadToFile();
					if (System.IO.File.Exists(filePath))
					{
						var fs = System.IO.File.OpenRead(filePath);
						return File(fs, "application/octet-stream");
					}
				}
			}
			catch (Exception ex)
			{
				 await _errorLog.LogError("HomeController -> DownloadFile", $"fileId = {fileId}", ex.Message);
				return BadRequest();
			}

			return BadRequest();
		}
	}
}
