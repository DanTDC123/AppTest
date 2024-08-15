using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TestModels;


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
            return await _httpClient.GetFromJsonAsync<List<Employee>>("/api/Employee/getEmployees");
        }

        public async Task<bool> DelEmployee(int id)
        {
			await _httpClient.DeleteAsync($"/api/Employee/delEmployees/{id}");
            return true;
		}

        public async Task<List<TestModels.Employee>> GetEmployeeByID(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<TestModels.Employee>>($"/api/Employee/getByID/{id}");
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
			}
            catch (Exception ex)
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
            }
            catch (Exception ex)
            {
            }
        }
    }
}
