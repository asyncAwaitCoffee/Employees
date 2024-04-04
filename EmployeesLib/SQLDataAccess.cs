using System.Configuration;
namespace EmployeesLib
{
	public static class SQLDataAccess
	{
		static SQLDataAccess()
		{
			var a = ConfigurationManager.AppSettings.AllKeys.Length;
			Console.WriteLine("aaa");
			Console.WriteLine(a);
			Console.WriteLine("aaa");
		}

		public static string Test { get; set; } = "abc";
	}
}
