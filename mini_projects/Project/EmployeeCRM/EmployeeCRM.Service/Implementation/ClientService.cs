using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.Service.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        // Get all clients
        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _repository.GetAllAsync();
                return clients.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all clients", ex);
            }
        }

        // Get client by ID
        public async Task<ClientDTO> GetClientByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid client ID");

                var client = await _repository.GetByIdAsync(id);

                if (client == null)
                    throw new KeyNotFoundException($"Client with ID {id} not found");

                return MapToDTO(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving client with ID {id}", ex);
            }
        }

        // Create new client
        public async Task<ClientDTO> CreateClientAsync(ClientDTO clientDTO)
        {
            try
            {
                // Validation
                if (clientDTO == null)
                    throw new ArgumentNullException(nameof(clientDTO));

                if (string.IsNullOrWhiteSpace(clientDTO.CompanyName))
                    throw new ArgumentException("Company name is required");

                if (string.IsNullOrWhiteSpace(clientDTO.Email))
                    throw new ArgumentException("Email is required");

                if (clientDTO.Rating < 0 || clientDTO.Rating > 5)
                    throw new ArgumentException("Rating must be between 0 and 5");

                // Map DTO to entity
                var client = new Client
                {
                    CompanyName = clientDTO.CompanyName,
                    ContactName = clientDTO.ContactName,
                    Email = clientDTO.Email,
                    Phone = clientDTO.Phone,
                    Address = clientDTO.Address,
                    City = clientDTO.City,
                    State = clientDTO.City,
                    ZipCode = clientDTO.City,
                    Rating = clientDTO.Rating,
                    EmployeeId = clientDTO.EmployeeId,
                    CreatedDate = DateTime.Now
                };

                var result = await _repository.AddAsync(client);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating client", ex);
            }
        }

        // Update client
        public async Task<ClientDTO> UpdateClientAsync(int id, ClientDTO clientDTO)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid client ID");

                if (clientDTO == null)
                    throw new ArgumentNullException(nameof(clientDTO));

                var client = await _repository.GetByIdAsync(id);

                if (client == null)
                    throw new KeyNotFoundException($"Client with ID {id} not found");

                // Update properties
                client.CompanyName = clientDTO.CompanyName;
                client.ContactName = clientDTO.ContactName;
                client.Email = clientDTO.Email;
                client.Phone = clientDTO.Phone;
                client.Address = clientDTO.Address;
                client.City = clientDTO.City;
                client.Rating = clientDTO.Rating;
                client.EmployeeId = clientDTO.EmployeeId;
                client.UpdatedDate = DateTime.Now;

                var result = await _repository.UpdateAsync(client);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating client with ID {id}", ex);
            }
        }

        // Delete client
        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid client ID");

                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting client with ID {id}", ex);
            }
        }

        // Search clients
        public async Task<IEnumerable<ClientDTO>> SearchClientsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    throw new ArgumentException("Search term cannot be empty");

                var clients = await _repository.SearchAsync(searchTerm);
                return clients.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching clients", ex);
            }
        }

        // Get clients by employee
        public async Task<IEnumerable<ClientDTO>> GetClientsByEmployeeAsync(int employeeId)
        {
            try
            {
                if (employeeId <= 0)
                    throw new ArgumentException("Invalid employee ID");

                var clients = await _repository.GetByEmployeeAsync(employeeId);
                return clients.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving clients by employee", ex);
            }
        }

        // Helper method: Map Entity to DTO
        private ClientDTO MapToDTO(Client client)
        {
            return new ClientDTO
            {
                Id = client.Id,
                CompanyName = client.CompanyName,
                ContactName = client.ContactName,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                Rating = client.Rating,
                EmployeeId = client.EmployeeId,
                CreatedDate = client.CreatedDate
            };
        }
    }
}