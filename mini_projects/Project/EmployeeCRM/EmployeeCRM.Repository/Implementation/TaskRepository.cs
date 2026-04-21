using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;
using EmployeeCRM.Data;
using TaskStatus = EmployeeCRM.Core.Enums.TaskStatus;


namespace EmployeeCRM.Repository.Implementation
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        // Get tasks by employee
        public async Task<IEnumerable<TaskItem>> GetByEmployeeAsync(int employeeId)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.EmployeeId == employeeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks by employee", ex);
            }
        }

        // Get tasks by status
        public async Task<IEnumerable<TaskItem>> GetByStatusAsync(TaskStatus status)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.Status == status)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks by status", ex);
            }
        }

        // Get tasks by client
        public async Task<IEnumerable<TaskItem>> GetByClientAsync(int clientId)
        {
            try
            {
                return await _dbSet
                    .Where(t => t.ClientId == clientId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tasks by client", ex);
            }
        }

        // Get overdue tasks (due date passed and not completed)
        public async Task<IEnumerable<TaskItem>> GetOverdueTasksAsync()
        {
            try
            {
                return await _dbSet
                    .Where(t => t.DueDate < DateTime.Now &&
                               t.Status != TaskStatus.Completed)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving overdue tasks", ex);
            }
        }

        //Error handled by copilot for the line 15 ITaskRepository
        public Task<IEnumerable<TaskItem>> GetByStatusAsync(System.Threading.Tasks.TaskStatus status)
        {
            throw new NotImplementedException();
        }
    }
}