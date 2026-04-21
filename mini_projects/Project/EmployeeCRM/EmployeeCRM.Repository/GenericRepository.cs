using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;

namespace EmployeeCRM.Repository
{
    /// <summary>
    /// Generic repository implementation
    /// Provides basic CRUD operations for any entity
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Get all records
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all records from {typeof(T).Name}", ex);
            }
        }

        // Get by ID
        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving {typeof(T).Name} with id {id}", ex);
            }
        }

        // Get with filtering using LINQ
        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error filtering {typeof(T).Name} records", ex);
            }
        }

        // Add new record
        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding {typeof(T).Name}", ex);
            }
        }

        // Update record
        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating {typeof(T).Name}", ex);
            }
        }

        // Delete record
        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting {typeof(T).Name} with id {id}", ex);
            }
        }

        // Check if exists
        public virtual async Task<bool> ExistsAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                return entity != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking existence of {typeof(T).Name}", ex);
            }
        }
    }
}