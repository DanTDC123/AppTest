using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestModels;

namespace Test.Services
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetEmployee();

        Task<bool> DelEmployee(int id);

        Task<TestModels.Employee> GetEmployeeByID(int id);

        //Task<bool> AddEmployee(int id);

        //Task<bool> UpdateEmployee(int id);

	}
}
