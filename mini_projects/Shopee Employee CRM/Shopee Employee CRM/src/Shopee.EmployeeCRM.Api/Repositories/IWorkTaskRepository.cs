using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public interface IWorkTaskRepository
{
    Task<List<WorkTask>> GetAllAsync();
    Task<WorkTask?> GetByIdAsync(int id);
    Task<WorkTask> AddAsync(WorkTask task);
    Task<bool> UpdateAsync(WorkTask task);
    Task<bool> DeleteAsync(int id);
}
