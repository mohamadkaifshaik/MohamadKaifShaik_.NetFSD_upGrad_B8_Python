using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public interface IClientRepository
{
    Task<List<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client> AddAsync(Client client);
    Task<bool> UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
}
