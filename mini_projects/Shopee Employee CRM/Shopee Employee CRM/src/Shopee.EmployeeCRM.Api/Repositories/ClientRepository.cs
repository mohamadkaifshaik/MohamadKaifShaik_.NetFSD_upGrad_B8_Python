using Microsoft.EntityFrameworkCore;
using Shopee.EmployeeCRM.Api.Data;
using Shopee.EmployeeCRM.Api.Entities;

namespace Shopee.EmployeeCRM.Api.Repositories;

public class ClientRepository(AppDbContext context) : IClientRepository
{
    public async Task<List<Client>> GetAllAsync() =>
        await context.Clients
            .Include(client => client.Employee)
            .OrderBy(client => client.CompanyName)
            .ToListAsync();

    public Task<Client?> GetByIdAsync(int id) =>
        context.Clients
            .Include(client => client.Employee)
            .FirstOrDefaultAsync(client => client.Id == id);

    public async Task<Client> AddAsync(Client client)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync();
        return client;
    }

    public async Task<bool> UpdateAsync(Client client)
    {
        var existingClient = await context.Clients.FindAsync(client.Id);
        if (existingClient is null)
        {
            return false;
        }

        existingClient.CompanyName = client.CompanyName;
        existingClient.ContactPerson = client.ContactPerson;
        existingClient.Email = client.Email;
        existingClient.PhoneNumber = client.PhoneNumber;
        existingClient.EmployeeId = client.EmployeeId;
        existingClient.Notes = client.Notes;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var client = await context.Clients.FindAsync(id);
        if (client is null)
        {
            return false;
        }

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
        return true;
    }
}
