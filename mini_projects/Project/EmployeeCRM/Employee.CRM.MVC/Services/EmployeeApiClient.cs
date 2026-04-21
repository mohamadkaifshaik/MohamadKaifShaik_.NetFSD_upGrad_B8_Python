using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmployeeApiClient> _logger;

        public EmployeeApiClient(HttpClient httpClient, ILogger<EmployeeApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private void AddAuthHeader(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync(string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync("api/employees");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<EmployeeDTO>>(content, options);
                }

                _logger.LogError($"Error getting all employees: {response.StatusCode}");
                return new List<EmployeeDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/employees/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<EmployeeDTO>(content, options);
                }

                _logger.LogError($"Error getting employee {id}: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> SearchAsync(string searchTerm, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/employees/search/{searchTerm}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<EmployeeDTO>>(content, options);
                }

                _logger.LogError($"Error searching employees: {response.StatusCode}");
                return new List<EmployeeDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO employeeDTO, string token)
        {
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(employeeDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/employees", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<EmployeeDTO>(responseContent, options);
                }

                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error creating employee: {error}");
                throw new Exception("Failed to create employee");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDTO> UpdateAsync(int id, EmployeeDTO employeeDTO, string token)
        {
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(employeeDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/employees/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<EmployeeDTO>(responseContent, options);
                }

                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error updating employee: {error}");
                throw new Exception("Failed to update employee");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.DeleteAsync($"api/employees/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error deleting employee: {error}");
                    throw new Exception("Failed to delete employee");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Employee API error: {ex.Message}");
                throw;
            }
        }
    }
}