using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public interface IEmployeeApiClient
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync(string token);
        Task<EmployeeDTO> GetByIdAsync(int id, string token);
        Task<IEnumerable<EmployeeDTO>> SearchAsync(string searchTerm, string token);
        Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeDTO, string token);
        Task<EmployeeDTO> UpdateAsync(int id, EmployeeDTO employeeDTO, string token);
        Task DeleteAsync(int id, string token);
    }
}