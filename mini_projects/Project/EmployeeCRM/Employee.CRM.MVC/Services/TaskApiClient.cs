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
    public class TaskApiClient : ITaskApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TaskApiClient> _logger;

        public TaskApiClient(HttpClient httpClient, ILogger<TaskApiClient> logger)
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

        public async Task<IEnumerable<TaskDTO>> GetAllAsync(string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync("api/tasks");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<TaskDTO>>(content, options);
                }

                return new List<TaskDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskDTO> GetByIdAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/tasks/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<TaskDTO>(content, options);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<TaskDTO>> GetByEmployeeAsync(int employeeId, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/tasks/employee/{employeeId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<TaskDTO>>(content, options);
                }

                return new List<TaskDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskDTO> CreateAsync(TaskDTO taskDTO, string token)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //var response = await _httpClient.PostAsJsonAsync("api/tasks", taskDTO);
            //var content = await response.Content.ReadAsStringAsync();
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new Exception(content);
            //}
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(taskDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/tasks", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<TaskDTO>(responseContent, options);
                }

                throw new Exception("Failed to create task");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskDTO> UpdateAsync(int id, TaskDTO taskDTO, string token)
        {
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(taskDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/tasks/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<TaskDTO>(responseContent, options);
                }

                throw new Exception("Failed to update task");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.DeleteAsync($"api/tasks/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Failed to delete task");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Task API error: {ex.Message}");
                throw;
            }
        }
    }
}