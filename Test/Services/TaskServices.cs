using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestModels;
using System.Web;
using System.Web.Http;

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
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<TaskModel>>("/api/Task/getTasks");
				if (response == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (!response.Equals(new List<Employee>()))
				{
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
				return response;
			}
			catch { return null; }
		}

		public async Task<bool> DelTask(int id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/Task/delTasks/{id}");
				if (response.StatusCode == HttpStatusCode.BadGateway)
				{
					return false;
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (response.StatusCode == HttpStatusCode.InternalServerError)
				{
					return false;
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
				return true;
			} catch { return false; }
		}

		public async Task<List<TestModels.TaskModel>> GetByID(int id)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<TestModels.TaskModel>>($"/api/Task/getByID/{id}");
				if (response == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (!response.Equals(new List<Employee>()))
				{
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
				return response;
			}
			catch { return null; }
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
				if (response.StatusCode == HttpStatusCode.BadGateway)
				{
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (response.StatusCode == HttpStatusCode.InternalServerError)
				{
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
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
				if (response.StatusCode == HttpStatusCode.BadGateway)
				{
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (response.StatusCode == HttpStatusCode.InternalServerError)
				{
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
			}
			catch (Exception ex) { }
		}
	}
}
