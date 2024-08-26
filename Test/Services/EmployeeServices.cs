
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
    public class EmployeeServices : IEmployeeServices
    {
        private readonly HttpClient _httpClient;

        public EmployeeServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetEmployee()
        {
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<Employee>>("/api/Employee/getEmployees");
				if (response == null)
				{
					throw new HttpResponseException(HttpStatusCode.BadGateway);
				}
				else if (!response.Equals(new List<Employee>()))
				{
					throw new HttpResponseException(HttpStatusCode.InternalServerError);
				}
				return response;
			} catch { return null; }
        }

		public async Task<List<Employee>> GetEAT()
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<Employee>>("/api/Employee/getEAT");
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

		public async Task<bool> DelEmployee(int id)
        {
            try
            {
				var response = await _httpClient.DeleteAsync($"/api/Employee/delEmployees/{id}");
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

        public async Task<List<TestModels.Employee>> GetEmployeeByID(int id)
        {
			try
			{
				var response = await _httpClient.GetFromJsonAsync<List<TestModels.Employee>>($"/api/Employee/getByID/{id}");
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

        public async Task UpEmployee(Employee emp)
        {
            try
            {
				var id = emp.EID;
				var json = JsonConvert.SerializeObject(emp);
				//, Encoding.UTF8, "application/json"
				var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Put, "/api/Employee/updateByID");
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

        public async Task AddEmployee(Employee emp)
        {
            try
            {
                var id = emp.EID;
                var json = JsonConvert.SerializeObject(emp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/Employee/addEmployee");
                request.Content = content;
                var response = await _httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.BadGateway)
                {
                    throw new HttpResponseException(HttpStatusCode.BadGateway);
                } else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            catch
            {
            }
        }
    }
}
