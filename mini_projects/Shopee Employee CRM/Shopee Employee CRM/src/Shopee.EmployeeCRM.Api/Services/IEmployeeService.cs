using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllAsync(string? searchText);
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<EmployeeDto> AddAsync(EmployeeDto employeeDto);
    Task<bool> UpdateAsync(int id, EmployeeDto employeeDto);
    Task<bool> DeleteAsync(int id);
}
