using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;

namespace EmployeeCRM.Repository.Implementation
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        // Search clients by company name or contact
        public async Task<IEnumerable<Client>> SearchAsync(string searchTerm)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.CompanyName.Contains(searchTerm) ||
                               c.ContactName.Contains(searchTerm) ||
                               c.Email.Contains(searchTerm))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching clients", ex);
            }
        }

        // Get clients by employee
        public async Task<IEnumerable<Client>> GetByEmployeeAsync(int employeeId)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.EmployeeId == employeeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving clients by employee", ex);
            }
        }

        // Get clients by rating
        public async Task<IEnumerable<Client>> GetByRatingAsync(int rating)
        {
            try
            {
                return await _dbSet
                    .Where(c => c.Rating == rating)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving clients by rating", ex);
            }
        }
    }
}