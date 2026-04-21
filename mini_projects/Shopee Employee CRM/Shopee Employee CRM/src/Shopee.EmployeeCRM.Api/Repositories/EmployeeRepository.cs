using Microsoft.EntityFrameworkCore;
using Shopee.EmployeeCRM.Api.Data;
using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<List<Employee>> GetAllAsync(string? searchText)
    {
        var query = context.Employees
            .Include(employee => employee.Clients)
            .Include(employee => employee.Tasks)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(employee =>
                employee.FullName.Contains(searchText) ||
                employee.Department.Contains(searchText) ||
                employee.Designation.Contains(searchText));
        }

        return await query.OrderBy(employee => employee.FullName).ToListAsync();
    }

    public Task<Employee?> GetByIdAsync(int id) =>
        context.Employees
            .Include(employee => employee.Clients)
            .Include(employee => employee.Tasks)
            .FirstOrDefaultAsync(employee => employee.Id == id);

    public async Task<Employee> AddAsync(Employee employee)
    {
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> UpdateAsync(Employee employee)
    {
        var existingEmployee = await context.Employees.FindAsync(employee.Id);
        if (existingEmployee is null)
        {
            return false;
        }

        existingEmployee.FullName = employee.FullName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Department = employee.Department;
        existingEmployee.Designation = employee.Designation;
        existingEmployee.Role = employee.Role;
        existingEmployee.JoinedOn = employee.JoinedOn;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await context.Employees
            .Include(item => item.Clients)
            .Include(item => item.Tasks)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (employee is null)
        {
            return false;
        }

        if (employee.Clients.Any() || employee.Tasks.Any())
        {
            return false;
        }

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return true;
    }
}
