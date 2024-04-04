using EmployeesApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Controllers
{
	public class HomeController(DataAccess dataAccess) : Controller
	{
		private readonly DataAccess _dataAccess = dataAccess;

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

		public async Task<IActionResult> DownloadFile(int fileId)
		{
			// TODO - System.IO.File.Exists(filePath);
			if (fileId == 2)
			{
				await _dataAccess.CompaniesLoadToFile();
				//var fileExists = System.IO.File.Exists(filePath);
				var fs = System.IO.File.OpenRead(@"D:\MyTemp\COMPANIES_full.csv");
				return File(fs, "application/octet-stream");
			}

			if (fileId == 3)
			{
				await _dataAccess.EmployeesLoadToFile();
				//var fileExists = System.IO.File.Exists(filePath);
				var fs = System.IO.File.OpenRead(@"D:\MyTemp\EMPLOYEES_full.csv");
				return File(fs, "application/octet-stream");
			}

			return BadRequest();
		}
	}
}
