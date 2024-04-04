﻿using EmployeesApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace EmployeesApp.Services
{
	public class DataAccess
	{
        private readonly IConfiguration _configuration;
		private readonly Regex _regexIsCSV = new(@"\.csv$", RegexOptions.IgnoreCase);
		private readonly string _loadDirectory = @"D:\MyTemp";

		public DataAccess(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task CompaniesAdd(CompanyModel company)
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.COMPANIES_ADD";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				var (name, inn, legal, fact) = company;

				command.Parameters.AddRange([
					new("@COMPANY_NAME", name),
						new("@INN", inn),
						new("@LEGAL_ADRESS", legal),
						new("@FACT_ADRESS", fact)
				]);

				var r = await command.ExecuteNonQueryAsync();
				Console.WriteLine(r);
			}
		}

		public async Task EmployeesAdd(EmployeeModel employee)
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.EMPLOYEES_ADD";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				var (name, surname, patronymic, birthdate, passportSeries, passportNumber, companyId) = employee;

				command.Parameters.AddRange([
					new("@EMPLOYEE_NAME", name),
					new("@SURNAME", surname),
					new("@PATRONYMIC", patronymic),
					new("@BIRTHDATE", birthdate),
					new("@PASSPORT_SERIES", passportSeries),
					new("@PASSPORT_NUMBER", passportNumber),
					new("@COMPANY_ID", companyId)
				]);

				var r = await command.ExecuteNonQueryAsync();
				Console.WriteLine(r);
			}
		}

		public async Task<List<CompanyModel>> CompaniesGetAll()
		{
			List<CompanyModel> companies = [];

			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.COMPANIES_GET_ALL";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				SqlDataReader result = await command.ExecuteReaderAsync();

				while (await result.ReadAsync())
				{
					companies.Add(
							new()
							{
								CompanyId = result.GetInt32("COMPANY_ID"),
								Name = result.GetString("COMPANY_NAME"),
								Inn = result.GetString("INN"),
								Legal = result.GetString("LEGAL_ADRESS"),
								Fact = result.GetString("FACT_ADRESS"),
							}
						);
				}
			}

			return companies;
		}

		public async Task<CompanyModel> CompaniesGetById(int companyId)
		{
			CompanyModel company = new();

			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.COMPANIES_GET_BY_ID";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure,
					Parameters = { new("@COMPANY_ID", companyId) }
				};

				SqlDataReader result = await command.ExecuteReaderAsync(CommandBehavior.SingleResult);

				while (await result.ReadAsync())
				{
					company = new()
					{
						Name = result.GetString("COMPANY_NAME"),
						Inn = result.GetString("INN"),
						Legal = result.GetString("LEGAL_ADRESS"),
						Fact = result.GetString("FACT_ADRESS"),
					};
				}
			}
			return company;
		}

		public async Task<EmployeeModel> EmployeesGetById(int employeeId)
		{
			EmployeeModel employee = new();

			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.EMPLOYEES_GET_BY_ID";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure,
					Parameters = { new("@EMPLOYEE_ID", employeeId) }
				};

				SqlDataReader result = await command.ExecuteReaderAsync(CommandBehavior.SingleResult);

				while (await result.ReadAsync())
				{
					employee = new()
					{
						FirstName = result.GetString("EMPLOYEE_NAME"),
						Surname = result.GetString("SURNAME"),
						Patronymic = result.GetString("PATRONYMIC"),
						Birthdate = DateOnly.FromDateTime(result.GetDateTime("BIRTHDATE")),
						Series = result.GetString("PASSPORT_SERIES"),
						Number = result.GetString("PASSPORT_NUMBER")
					};
				}
			}
			return employee;
		}

		public async Task<List<EmployeeModel>> EmployeesGetByCompany(int companyId)
		{
			List<EmployeeModel> employees = [];

			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.EMPLOYEES_GET_BY_COMPANY";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				command.Parameters.AddRange([
					new("@COMPANY_ID", companyId)
				]);

				SqlDataReader result = await command.ExecuteReaderAsync();

				while (await result.ReadAsync())
				{
					employees.Add(
							new()
							{
								EmployeeId = result.GetInt32("EMPLOYEE_ID"),
								FirstName = result.GetString("EMPLOYEE_NAME"),
								Surname = result.GetString("SURNAME"),
								Patronymic = result.GetString("PATRONYMIC"),
								Birthdate = DateOnly.FromDateTime(result.GetDateTime("BIRTHDATE")),
								Series = result.GetString("PASSPORT_SERIES"),
								Number = result.GetString("PASSPORT_NUMBER"),
								CompanyId = companyId
							}
						);
				}
			}

			return employees;
		}

		public async Task CompaniesLoadFromFile(string filePath)
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.COMPANIES_LOAD_FROM_FILE";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				command.Parameters.AddRange([
					new("@FILE_PATH", filePath)
				]);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task EmployeesLoadFromFile(string filePath)
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.EMPLOYEES_LOAD_FROM_FILE";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				command.Parameters.AddRange([
					new("@FILE_PATH", filePath)
				]);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task CompaniesLoadToFile()
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.COMPANIES_LOAD_TO_FILE";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task EmployeesLoadToFile()
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.EMPLOYEES_LOAD_TO_FILE";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure
				};

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task<string> LoadFile(IFormFile file)
		{
			if (!_regexIsCSV.IsMatch(file.FileName))
			{
				throw new FileLoadException();
			}

			if (!Directory.Exists(_loadDirectory))
			{
				Directory.CreateDirectory(_loadDirectory);
			}

			string fileName =
				$"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			string filePath = Path.Combine(_loadDirectory, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			return filePath;
		}
	}
}
