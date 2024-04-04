using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeesApp.Services
{
	public class ErrorLog(IConfiguration configuration)
	{
		private readonly IConfiguration _configuration = configuration;

		public async Task LogError(string context, string parameters, string message)
		{
			using (SqlConnection connection = new(_configuration.GetConnectionString("Default")))
			{
				connection.Open();
				string query = "DEPO.LOG_ERROR";

				SqlCommand command = new(query, connection)
				{
					CommandType = CommandType.StoredProcedure,
					Parameters = {
						new("@CONTEXT", context),
						new("@PARAMS", parameters),
						new("@ERROR_TEXT", message)
					}
				};

				await command.ExecuteNonQueryAsync();
			}
		}
	}
}
