using Newtonsoft.Json;
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

		public async Task UpTask(TestModels.TaskModel task)
		{
			try
			{
				var id = task.TID;
				var json = JsonConvert.SerializeObject(task);
				//, Encoding.UTF8, "application/json"
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var request = new HttpRequestMessage(HttpMethod.Put, "/api/Task/updateByID");
				request.Content = content;
				//var response = await _httpClient.PutAsync($"/api/Employee/updateByID", content);
				var response = await _httpClient.SendAsync(request);
			}
			catch
			{
			}
		}

		public async Task AddTask(TestModels.TaskModel task)
		{
			try
			{
				var id = task.TID;
				var json = JsonConvert.SerializeObject(task);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var request = new HttpRequestMessage(HttpMethod.Post, "api/Task/addTask");
				request.Content = content;
				var response = await _httpClient.SendAsync(request);
			}
			catch (Exception ex) { }
		}
	}
}
