using EmployeeCRM.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDTO>> SearchEmployeesAsync(string searchTerm);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByStatusAsync(int statusId);
    }
}
