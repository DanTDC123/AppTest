using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestModels;

namespace Test.Services
{
	public class TaskServices : ITaskServices
	{
		private readonly HttpClient _httpClient;

		public TaskServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<TaskModel>> GetTask()
		{
			return await _httpClient.GetFromJsonAsync<List<TaskModel>>("/api/Task/getTasks");
		}

		public async Task<bool> DelTask(int id)
		{
			await _httpClient.DeleteAsync($"/api/Task/delTasks/{id}");
			return true;
		}

		public async Task<List<TestModels.TaskModel>> GetByID(int id)
		{
			return await _httpClient.GetFromJsonAsync<List<TestModels.TaskModel>>($"/api/Task/getByID/{id}");
		}
	}
}
