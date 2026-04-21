using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public interface IClientApiClient
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync(string token);
        Task<ClientDTO> GetByIdAsync(int id, string token);
        Task<IEnumerable<ClientDTO>> SearchAsync(string searchTerm, string token);
        Task<ClientDTO> CreateAsync(ClientDTO clientDTO, string token);
        Task<ClientDTO> UpdateAsync(int id, ClientDTO clientDTO, string token);
        Task DeleteAsync(int id, string token);
    }
}