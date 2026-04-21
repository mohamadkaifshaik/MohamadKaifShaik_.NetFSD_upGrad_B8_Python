using EmployeeCRM.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.Core.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetAllTasksAsync();
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO);
        Task<TaskDTO> UpdateTaskAsync(int id, TaskDTO taskDTO);
        Task<bool> DeleteTaskAsync(int id);
        Task<IEnumerable<TaskDTO>> GetTasksByEmployeeAsync(int employeeId);
        Task<IEnumerable<TaskDTO>> GetTasksByStatusAsync(TaskStatusEnum status);
        Task<IEnumerable<TaskDTO>> GetOverdueTasksAsync();
        
    }
}
