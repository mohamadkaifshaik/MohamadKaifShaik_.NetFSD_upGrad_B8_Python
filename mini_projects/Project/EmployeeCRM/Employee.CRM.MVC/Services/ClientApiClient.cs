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
    public class ClientApiClient : IClientApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClientApiClient> _logger;

        public ClientApiClient(HttpClient httpClient, ILogger<ClientApiClient> logger)
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

        public async Task<IEnumerable<ClientDTO>> GetAllAsync(string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync("api/clients");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<ClientDTO>>(content, options);
                }

                return new List<ClientDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }

        public async Task<ClientDTO> GetByIdAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/clients/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ClientDTO>(content, options);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ClientDTO>> SearchAsync(string searchTerm, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.GetAsync($"api/clients/search/{searchTerm}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<IEnumerable<ClientDTO>>(content, options);
                }

                return new List<ClientDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }

        public async Task<ClientDTO> CreateAsync(ClientDTO clientDTO, string token)
        {
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(clientDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/clients", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ClientDTO>(responseContent, options);
                }

                throw new Exception("Failed to create client");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }

        public async Task<ClientDTO> UpdateAsync(int id, ClientDTO clientDTO, string token)
        {
            try
            {
                AddAuthHeader(token);
                var json = JsonSerializer.Serialize(clientDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"api/clients/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ClientDTO>(responseContent, options);
                }

                throw new Exception("Failed to update client");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(int id, string token)
        {
            try
            {
                AddAuthHeader(token);
                var response = await _httpClient.DeleteAsync($"api/clients/{id}");

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Failed to delete client");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client API error: {ex.Message}");
                throw;
            }
        }
    }
}