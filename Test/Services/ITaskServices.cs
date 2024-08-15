using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestModels;

namespace Test.Services
{
	public interface ITaskServices
	{
		Task<List<TaskModel>> GetTask();

		Task<bool> DelTask(int id);

        Task<List<TestModels.TaskModel>> GetByID(int id);

		Task UpTask(TestModels.TaskModel task);

		Task AddTask(TestModels.TaskModel task);
	}
}
