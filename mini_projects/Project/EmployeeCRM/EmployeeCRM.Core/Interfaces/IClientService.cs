using EmployeeCRM.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync();
        Task<ClientDTO> GetClientByIdAsync(int id);
        Task<ClientDTO> CreateClientAsync(ClientDTO clientDTO);
        Task<ClientDTO> UpdateClientAsync(int id, ClientDTO clientDTO);
        Task<bool> DeleteClientAsync(int id);
        Task<IEnumerable<ClientDTO>> SearchClientsAsync(string searchTerm);
        Task<IEnumerable<ClientDTO>> GetClientsByEmployeeAsync(int employeeId);
    }
}
