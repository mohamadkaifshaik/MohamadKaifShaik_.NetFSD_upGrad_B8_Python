using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    /// <summary>
    /// Generic repository interface for CRUD operations
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        // Get all records
        Task<IEnumerable<T>> GetAllAsync();

        // Get by ID
        Task<T> GetByIdAsync(int id);

        // Get with filtering
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        // Add new record
        Task<T> AddAsync(T entity);

        // Update record
        Task<T> UpdateAsync(T entity);

        // Delete record
        Task<bool> DeleteAsync(int id);

        // Check if exists
        Task<bool> ExistsAsync(int id);
    }
}
