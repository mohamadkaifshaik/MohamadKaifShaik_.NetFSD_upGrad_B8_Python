using EmployeeCRM.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<LoginResponseDTO> RegisterAsync(RegisterDTO registerDTO);
        string GenerateJwtToken(UserDTO user);
    }
}
