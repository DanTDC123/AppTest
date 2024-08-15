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
	[Route("api/Task")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public TaskController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected IDbConnection GetConnection()
		{
			var con = new MySqlConnection(_configuration.GetConnectionString("defaultCon"));
			return con;

		}

		[HttpGet]
		[Route("getTasks")]
		public async Task<IEnumerable<TaskModel>> getTask()
		{
			var sql = "SELECT * FROM task";
			var con = GetConnection();
			var _listTask = await con.QueryAsync<TaskModel>(sql);
			return _listTask;
		}

		[HttpDelete]
		[Route("delTasks/{id}")]
		public async Task<bool> delTask(int id)
		{
			var sql = "DELETE FROM task WHERE TID=" + id;
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}

		[HttpGet]
		[Route("getByID/{id}")]
		public async Task<IEnumerable<TestModels.TaskModel>> getTaskByID(int id)
		{
			var sql = "SELECT * FROM task WHERE TID=" + id;
			var con = GetConnection();
			var _listTask = await con.QueryAsync<TestModels.TaskModel>(sql);
			return _listTask;
		}

		[HttpPut]
		[Route("updateByID")]
		public async Task<bool> updateTask([FromBody] TestModels.TaskModel task)
		{
			var sql = "UPDATE task SET Name='" + task.Name + "', Assignee='" + task.Assignee + "' WHERE TID=" + task.TID;
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}

		[HttpPost]
		[Route("addTask")]
		public async Task<bool> addTask([FromBody] TestModels.TaskModel task)
		{
			var sql = "Insert into task VALUES (" + task.TID + ", '" + task.Name + "', '" + task.Assignee + "');";
			var con = GetConnection();
			await con.QueryAsync(sql);
			return true;
		}
	}
}
