using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public interface IAuthApiClient
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<LoginResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}