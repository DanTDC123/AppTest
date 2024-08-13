using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestModels;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySqlConnector;
using Mysqlx.Crud;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Ocsp;



namespace TestAPI.Controllers
{
	[Route("api/Employee")]
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
			var con = new MySqlConnection(_configuration.GetConnectionString("defaultCon"));
			return con;

		}

		[HttpGet]
		[Route("getEmployees")]
		public async Task<IEnumerable<Employee>> getEmployees()
		{
			string sql = "SELECT * FROM employee";
			var con = GetConnection();
			var _listEmployees = await con.QueryAsync<Employee>(sql);
			return _listEmployees;
		}

		[HttpDelete]
		[Route("delEmployees/{id}")]
		public async Task<bool> delEmployees(int id)
		{
			var sql = "DELETE FROM employee WHERE EID=" + id;
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}

		[HttpGet]
		[Route("getByID/{id}")]
		public async Task<IEnumerable<Employee>> getEmployeeById(int id)
		{
			var sql = "SELECT * FROM employee WHERE EID=" + id;
			var con = GetConnection();
			var _listEmployees = await con.QueryAsync<Employee>(sql);
			return _listEmployees;
		}

		[HttpPatch]
		[Route("updateByID/{id}")]
		public async Task<bool> updateEmployee(int id, HttpRequest request)
		{
			Employee? emp = await request.ReadFromJsonAsync<TestModels.Employee>();
			var sql = "Update employee SET Name="+ emp.Name +", Dob="+ emp.Dob +", Title="+ emp.Title +", Task="+ emp.Task + " WHERE EID="+id;
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}
	}
}
