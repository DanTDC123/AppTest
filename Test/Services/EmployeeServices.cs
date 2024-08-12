using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<TestModels.Employee> GetEmployeeByID(int id)
        {
            return await _httpClient.GetFromJsonAsync<TestModels.Employee>($"/api/Employee/getByID/{id}");
        }
    }
}
