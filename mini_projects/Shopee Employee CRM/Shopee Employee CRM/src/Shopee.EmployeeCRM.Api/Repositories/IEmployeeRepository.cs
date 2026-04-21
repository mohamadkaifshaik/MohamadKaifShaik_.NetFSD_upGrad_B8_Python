using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync(string? searchText);
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> AddAsync(Employee employee);
    Task<bool> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}
