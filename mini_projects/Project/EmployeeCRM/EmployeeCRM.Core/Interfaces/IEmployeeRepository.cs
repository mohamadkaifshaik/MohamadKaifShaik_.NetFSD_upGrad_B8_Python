using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // Search employees
        Task<IEnumerable<Employee>> SearchAsync(string searchTerm);

        // Get employees by manager
        Task<IEnumerable<Employee>> GetByManagerAsync(int managerId);

        // Get employees by status
        Task<IEnumerable<Employee>> GetByStatusAsync(int statusId);
    }
}
