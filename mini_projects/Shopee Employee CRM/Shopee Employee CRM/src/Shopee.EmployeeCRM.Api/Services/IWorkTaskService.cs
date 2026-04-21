using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public interface IWorkTaskService
{
    Task<List<WorkTaskDto>> GetAllAsync();
    Task<WorkTaskDto?> GetByIdAsync(int id);
    Task<WorkTaskDto> AddAsync(WorkTaskDto taskDto);
    Task<bool> UpdateAsync(int id, WorkTaskDto taskDto);
    Task<bool> DeleteAsync(int id);
}
