using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public interface ITaskApiClient
    {
        Task<IEnumerable<TaskDTO>> GetAllAsync(string token);
        Task<TaskDTO> GetByIdAsync(int id, string token);
        Task<IEnumerable<TaskDTO>> GetByEmployeeAsync(int employeeId, string token);
        Task<TaskDTO> CreateAsync(TaskDTO taskDTO, string token);
        Task<TaskDTO> UpdateAsync(int id, TaskDTO taskDTO, string token);
        Task DeleteAsync(int id, string token);
    }
}