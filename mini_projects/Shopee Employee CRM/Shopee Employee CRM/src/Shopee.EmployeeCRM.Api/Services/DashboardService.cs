using Shopee.EmployeeCRM.Api.Repositories;
using Shopee.EmployeeCRM.Shared.Dtos;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Api.Services;

public class DashboardService(
    IEmployeeRepository employeeRepository,
    IClientRepository clientRepository,
    IWorkTaskRepository taskRepository) : IDashboardService
{
    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var employees = await employeeRepository.GetAllAsync(null);
        var clients = await clientRepository.GetAllAsync();
        var tasks = await taskRepository.GetAllAsync();

        return new DashboardSummaryDto
        {
            TotalEmployees = employees.Count,
            TotalClients = clients.Count,
            TotalTasks = tasks.Count,
            CompletedTasks = tasks.Count(task => task.Status == WorkStatus.Completed),
            TopEmployees = employees
                .OrderByDescending(employee => employee.Tasks.Count(task => task.Status == WorkStatus.Completed))
                .Take(3)
                .Select(employee => new EmployeeDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    Department = employee.Department,
                    Designation = employee.Designation,
                    Role = employee.Role,
                    JoinedOn = employee.JoinedOn,
                    AssignedClientsCount = employee.Clients.Count,
                    CompletedTasksCount = employee.Tasks.Count(task => task.Status == WorkStatus.Completed)
                })
                .ToList(),
            UpcomingTasks = tasks
                .Where(task => task.Status != WorkStatus.Completed)
                .OrderBy(task => task.DueDate)
                .Take(5)
                .Select(task => new WorkTaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    EmployeeId = task.EmployeeId,
                    EmployeeName = task.Employee?.FullName ?? string.Empty,
                    DueDate = task.DueDate,
                    Status = task.Status
                })
                .ToList()
        };
    }
}
