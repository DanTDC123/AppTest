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
        private readonly HttpClient httpClient;
        public EmployeeServices(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public async Task<List<Employee>> getEmployee()
        {
            return await httpClient.GetFromJsonAsync<List<Employee>>("api/Employee/getEmployees");
        }
    }
}
