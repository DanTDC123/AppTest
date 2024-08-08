using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestModels;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySqlConnector;



namespace TestAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public EmployeeController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected IDbConnection GetConnection()
		{
			using IDbConnection con = new MySqlConnection(_configuration.GetConnectionString("defaultCon"));
			return con;

		}

		[HttpGet]
		[Route("getEmployees")]
		public async Task<IEnumerable<List<Employee>>> getEmployees()
		{
			string sql = "SELECT * FROM employee";
			var con = GetConnection();
			var _listEmployees = await con.QueryAsync<List<Employee>>(sql) as IEnumerable<List<Employee>>;
			return _listEmployees;
		}
	}
}
