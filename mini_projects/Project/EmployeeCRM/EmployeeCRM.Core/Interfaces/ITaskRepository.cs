using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        // Get tasks by employee
        Task<IEnumerable<TaskItem>> GetByEmployeeAsync(int employeeId);

        // Get tasks by status
        Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status);

        // Get tasks by client
        Task<IEnumerable<TaskItem>> GetByClientAsync(int clientId);

        // Get overdue tasks
        Task<IEnumerable<TaskItem>> GetOverdueTasksAsync();
        Task<IEnumerable<TaskItem>> GetByStatusAsync(Enums.TaskStatus status);
    }
}
