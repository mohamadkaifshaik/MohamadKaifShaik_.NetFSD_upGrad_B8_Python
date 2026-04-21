using Shopee.EmployeeCRM.Api.Entities;
using Shopee.EmployeeCRM.Api.Repositories;
using Shopee.EmployeeCRM.Shared.Dtos;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Api.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<List<EmployeeDto>> GetAllAsync(string? searchText)
    {
        var employees = await employeeRepository.GetAllAsync(searchText);
        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await employeeRepository.GetByIdAsync(id);
        return employee is null ? null : MapToDto(employee);
    }

    public async Task<EmployeeDto> AddAsync(EmployeeDto employeeDto)
    {
        var employee = new Employee
        {
            FullName = employeeDto.FullName,
            Email = employeeDto.Email,
            Department = employeeDto.Department,
            Designation = employeeDto.Designation,
            Role = employeeDto.Role,
            JoinedOn = employeeDto.JoinedOn
        };

        var savedEmployee = await employeeRepository.AddAsync(employee);
        return MapToDto(savedEmployee);
    }

    public Task<bool> UpdateAsync(int id, EmployeeDto employeeDto)
    {
        employeeDto.Id = id;
        return employeeRepository.UpdateAsync(new Employee
        {
            Id = employeeDto.Id,
            FullName = employeeDto.FullName,
            Email = employeeDto.Email,
            Department = employeeDto.Department,
            Designation = employeeDto.Designation,
            Role = employeeDto.Role,
            JoinedOn = employeeDto.JoinedOn
        });
    }

    public Task<bool> DeleteAsync(int id) => employeeRepository.DeleteAsync(id);

    private static EmployeeDto MapToDto(Employee employee) =>
        new()
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
        };
}
