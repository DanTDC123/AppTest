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
using Newtonsoft.Json;



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

		[HttpGet]
		[Route("getEAT")]
		public async Task<IEnumerable<Employee>> getEAT()
		{
			string sql = "SELECT E.EID, E.Name, E.Dob, E.Title, T.Name as Task FROM task T JOIN employee E ON E.EID = T.Assignee";
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

		[HttpPut]
		[Route("updateByID")]
		public async Task<bool> updateEmployee([FromBody] Employee emp)
		{
			var sql = "UPDATE employee SET Name='" + emp.Name + "', Dob='" + emp.Dob + "', Title='" + emp.Title + "', Task='" + emp.Task + "' WHERE EID=" + emp.EID;
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}

		[HttpPost]
		[Route("addEmployee")]
		public async Task<bool> addEmployee([FromBody] Employee emp)
		{
			var sql = "Insert into employee VALUES (" + emp.EID + ", '" + emp.Name + "', '" + emp.Dob + "', '" + emp.Title + "', '" + emp.Task + "');";
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}
	}
}
