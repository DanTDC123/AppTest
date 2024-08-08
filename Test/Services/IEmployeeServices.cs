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
        Task<List<Employee>> getEmployee();
    }
}
