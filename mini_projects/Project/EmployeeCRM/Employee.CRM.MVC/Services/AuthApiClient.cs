using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public class AuthApiClient : IAuthApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthApiClient> _logger;

        public AuthApiClient(HttpClient httpClient, ILogger<AuthApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                var json = JsonSerializer.Serialize(loginDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<LoginResponseDTO>(responseContent, options);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Login error: {errorContent}");
                    return new LoginResponseDTO { Success = false, Message = "Invalid credentials" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login API error: {ex.Message}");
                throw;
            }
        }

        public async Task<LoginResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            try
            {
                var json = JsonSerializer.Serialize(registerDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<LoginResponseDTO>(responseContent, options);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Register error: {errorContent}");
                    return new LoginResponseDTO { Success = false, Message = "Registration failed" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Register API error: {ex.Message}");
                throw;
            }
        }
    }
}