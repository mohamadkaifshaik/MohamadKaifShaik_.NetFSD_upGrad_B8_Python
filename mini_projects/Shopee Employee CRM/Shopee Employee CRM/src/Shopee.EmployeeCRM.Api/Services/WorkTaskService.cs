using Shopee.EmployeeCRM.Api.Entities;
using Shopee.EmployeeCRM.Api.Repositories;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public class WorkTaskService(IWorkTaskRepository taskRepository) : IWorkTaskService
{
    public async Task<List<WorkTaskDto>> GetAllAsync()
    {
        var tasks = await taskRepository.GetAllAsync();
        return tasks.Select(MapToDto).ToList();
    }

    public async Task<WorkTaskDto?> GetByIdAsync(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        return task is null ? null : MapToDto(task);
    }

    public async Task<WorkTaskDto> AddAsync(WorkTaskDto taskDto)
    {
        var task = new WorkTask
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            EmployeeId = taskDto.EmployeeId,
            DueDate = taskDto.DueDate,
            Status = taskDto.Status
        };

        var savedTask = await taskRepository.AddAsync(task);
        var fullTask = await taskRepository.GetByIdAsync(savedTask.Id) ?? savedTask;
        return MapToDto(fullTask);
    }

    public Task<bool> UpdateAsync(int id, WorkTaskDto taskDto)
    {
        taskDto.Id = id;
        return taskRepository.UpdateAsync(new WorkTask
        {
            Id = taskDto.Id,
            Title = taskDto.Title,
            Description = taskDto.Description,
            EmployeeId = taskDto.EmployeeId,
            DueDate = taskDto.DueDate,
            Status = taskDto.Status
        });
    }

    public Task<bool> DeleteAsync(int id) => taskRepository.DeleteAsync(id);

    private static WorkTaskDto MapToDto(WorkTask task) =>
        new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            EmployeeId = task.EmployeeId,
            EmployeeName = task.Employee?.FullName ?? string.Empty,
            DueDate = task.DueDate,
            Status = task.Status
        };
}
