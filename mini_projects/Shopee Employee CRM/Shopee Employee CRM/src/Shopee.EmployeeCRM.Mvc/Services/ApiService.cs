using System.Net.Http.Json;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Mvc.Services;

public class ApiService(HttpClient httpClient) : IApiService
{
    public async Task<List<EmployeeDto>> GetEmployeesAsync(string? searchText = null)
    {
        var url = "api/employees";
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            url += $"?searchText={Uri.EscapeDataString(searchText)}";
        }

        return await httpClient.GetFromJsonAsync<List<EmployeeDto>>(url) ?? [];
    }

    public Task<EmployeeDto?> GetEmployeeByIdAsync(int id) =>
        httpClient.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");

    public async Task<bool> CreateEmployeeAsync(EmployeeDto employee)
    {
        var response = await httpClient.PostAsJsonAsync("api/employees", employee);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateEmployeeAsync(EmployeeDto employee)
    {
        var response = await httpClient.PutAsJsonAsync($"api/employees/{employee.Id}", employee);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/employees/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<ClientDto>> GetClientsAsync() =>
        await httpClient.GetFromJsonAsync<List<ClientDto>>("api/clients") ?? [];

    public Task<ClientDto?> GetClientByIdAsync(int id) =>
        httpClient.GetFromJsonAsync<ClientDto>($"api/clients/{id}");

    public async Task<bool> CreateClientAsync(ClientDto client)
    {
        var response = await httpClient.PostAsJsonAsync("api/clients", client);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateClientAsync(ClientDto client)
    {
        var response = await httpClient.PutAsJsonAsync($"api/clients/{client.Id}", client);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/clients/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<List<WorkTaskDto>> GetTasksAsync() =>
        await httpClient.GetFromJsonAsync<List<WorkTaskDto>>("api/tasks") ?? [];

    public Task<WorkTaskDto?> GetTaskByIdAsync(int id) =>
        httpClient.GetFromJsonAsync<WorkTaskDto>($"api/tasks/{id}");

    public async Task<bool> CreateTaskAsync(WorkTaskDto task)
    {
        var response = await httpClient.PostAsJsonAsync("api/tasks", task);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateTaskAsync(WorkTaskDto task)
    {
        var response = await httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/tasks/{id}");
        return response.IsSuccessStatusCode;
    }

    public Task<DashboardSummaryDto?> GetDashboardAsync() =>
        httpClient.GetFromJsonAsync<DashboardSummaryDto>("api/dashboard");
}
