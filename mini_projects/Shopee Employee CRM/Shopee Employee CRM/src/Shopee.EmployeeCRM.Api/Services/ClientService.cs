using Shopee.EmployeeCRM.Api.Entities;
using Shopee.EmployeeCRM.Api.Repositories;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return clients.Select(MapToDto).ToList();
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client is null ? null : MapToDto(client);
    }

    public async Task<ClientDto> AddAsync(ClientDto clientDto)
    {
        var client = new Client
        {
            CompanyName = clientDto.CompanyName,
            ContactPerson = clientDto.ContactPerson,
            Email = clientDto.Email,
            PhoneNumber = clientDto.PhoneNumber,
            EmployeeId = clientDto.EmployeeId,
            Notes = clientDto.Notes
        };

        var savedClient = await clientRepository.AddAsync(client);
        var fullClient = await clientRepository.GetByIdAsync(savedClient.Id) ?? savedClient;
        return MapToDto(fullClient);
    }

    public Task<bool> UpdateAsync(int id, ClientDto clientDto)
    {
        clientDto.Id = id;
        return clientRepository.UpdateAsync(new Client
        {
            Id = clientDto.Id,
            CompanyName = clientDto.CompanyName,
            ContactPerson = clientDto.ContactPerson,
            Email = clientDto.Email,
            PhoneNumber = clientDto.PhoneNumber,
            EmployeeId = clientDto.EmployeeId,
            Notes = clientDto.Notes
        });
    }

    public Task<bool> DeleteAsync(int id) => clientRepository.DeleteAsync(id);

    private static ClientDto MapToDto(Client client) =>
        new()
        {
            Id = client.Id,
            CompanyName = client.CompanyName,
            ContactPerson = client.ContactPerson,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            EmployeeId = client.EmployeeId,
            EmployeeName = client.Employee?.FullName ?? string.Empty,
            Notes = client.Notes
        };
}
