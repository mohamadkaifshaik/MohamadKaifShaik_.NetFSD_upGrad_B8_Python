using EmployeeCRM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // Get user by email
        Task<User> GetByEmailAsync(string email);

        // Verify password
        bool VerifyPassword(string password, string hash);

        // Hash password
        string HashPassword(string password);
    }
}
