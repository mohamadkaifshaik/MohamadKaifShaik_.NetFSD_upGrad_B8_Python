using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.Services;

public interface IApiService
{
    Task<List<EmployeeDto>> GetEmployeesAsync(string? searchText = null);
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<bool> CreateEmployeeAsync(EmployeeDto employee);
    Task<bool> UpdateEmployeeAsync(EmployeeDto employee);
    Task<bool> DeleteEmployeeAsync(int id);
    Task<List<ClientDto>> GetClientsAsync();
    Task<ClientDto?> GetClientByIdAsync(int id);
    Task<bool> CreateClientAsync(ClientDto client);
    Task<bool> UpdateClientAsync(ClientDto client);
    Task<bool> DeleteClientAsync(int id);
    Task<List<WorkTaskDto>> GetTasksAsync();
    Task<WorkTaskDto?> GetTaskByIdAsync(int id);
    Task<bool> CreateTaskAsync(WorkTaskDto task);
    Task<bool> UpdateTaskAsync(WorkTaskDto task);
    Task<bool> DeleteTaskAsync(int id);
    Task<DashboardSummaryDto?> GetDashboardAsync();
}
