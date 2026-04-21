using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public interface IClientService
{
    Task<List<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(int id);
    Task<ClientDto> AddAsync(ClientDto clientDto);
    Task<bool> UpdateAsync(int id, ClientDto clientDto);
    Task<bool> DeleteAsync(int id);
}
