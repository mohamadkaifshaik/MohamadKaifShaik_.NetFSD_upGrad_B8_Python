using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public class DashboardApiClient : IDashboardApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DashboardApiClient> _logger;

        public DashboardApiClient(HttpClient httpClient, ILogger<DashboardApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<DashboardDTO> GetMetricsAsync(string token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.GetAsync("api/dashboard/metrics");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<DashboardDTO>(content, options);
                }

                return new DashboardDTO();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Dashboard API error: {ex.Message}");
                throw;
            }
        }
    }
}