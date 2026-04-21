using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;

namespace EmployeeCRM.Repository.Implementation
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        // Search employees by name or email
        public async Task<IEnumerable<Employee>> SearchAsync(string searchTerm)
        {
            try
            {
                return await _dbSet
                    .Where(e => e.FirstName.Contains(searchTerm) ||
                               e.LastName.Contains(searchTerm) ||
                               e.Email.Contains(searchTerm))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees", ex);
            }
        }

        // Get employees by manager
        public async Task<IEnumerable<Employee>> GetByManagerAsync(int managerId)
        {
            try
            {
                return await _dbSet
                    .Where(e => e.ManagerId == managerId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees by manager", ex);
            }
        }

        // Get employees by status
        public async Task<IEnumerable<Employee>> GetByStatusAsync(int statusId)
        {
            try
            {
                var status = (EmployeeStatus)statusId;
                return await _dbSet
                    .Where(e => e.Status == status)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees by status", ex);
            }
        }
    }
}