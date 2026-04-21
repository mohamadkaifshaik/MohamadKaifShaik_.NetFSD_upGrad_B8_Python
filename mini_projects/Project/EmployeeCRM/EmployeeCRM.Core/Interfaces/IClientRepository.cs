using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        // Search clients
        Task<IEnumerable<Client>> SearchAsync(string searchTerm);

        // Get clients by employee
        Task<IEnumerable<Client>> GetByEmployeeAsync(int employeeId);

        // Get clients by rating
        Task<IEnumerable<Client>> GetByRatingAsync(int rating);
    }
}
