using Microsoft.EntityFrameworkCore;
using Shopee.EmployeeCRM.Api.Data;
using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public class WorkTaskRepository(AppDbContext context) : IWorkTaskRepository
{
    public async Task<List<WorkTask>> GetAllAsync() =>
        await context.WorkTasks
            .Include(task => task.Employee)
            .OrderBy(task => task.DueDate)
            .ToListAsync();

    public Task<WorkTask?> GetByIdAsync(int id) =>
        context.WorkTasks
            .Include(task => task.Employee)
            .FirstOrDefaultAsync(task => task.Id == id);

    public async Task<WorkTask> AddAsync(WorkTask task)
    {
        context.WorkTasks.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(WorkTask task)
    {
        var existingTask = await context.WorkTasks.FindAsync(task.Id);
        if (existingTask is null)
        {
            return false;
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.EmployeeId = task.EmployeeId;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await context.WorkTasks.FindAsync(id);
        if (task is null)
        {
            return false;
        }

        context.WorkTasks.Remove(task);
        await context.SaveChangesAsync();
        return true;
    }
}
