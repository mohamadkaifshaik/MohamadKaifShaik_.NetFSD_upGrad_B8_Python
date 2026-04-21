using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Entities;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Get all employees
        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _repository.GetAllAsync();
                return employees.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all employees", ex);
            }
        }

        // Get employee by ID
        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid employee ID");

                var employee = await _repository.GetByIdAsync(id);

                if (employee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found");

                return MapToDTO(employee);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving employee with ID {id}", ex);
            }
        }

        // Create new employee
        public async Task<EmployeeDTO> CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            try
            {
                // Validation
                if (employeeDTO == null)
                    throw new ArgumentNullException(nameof(employeeDTO));

                if (string.IsNullOrWhiteSpace(employeeDTO.FirstName))
                    throw new ArgumentException("First name is required");

                if (string.IsNullOrWhiteSpace(employeeDTO.Email))
                    throw new ArgumentException("Email is required");

                if (employeeDTO.Salary < 0)
                    throw new ArgumentException("Salary cannot be negative");

                // Check if email already exists
                var existing = await _repository.SearchAsync(employeeDTO.Email);
                if (existing.Any(e => e.Email == employeeDTO.Email))
                    throw new InvalidOperationException("Email already exists");

                // Map DTO to entity
                var employee = new Employee
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    Email = employeeDTO.Email,
                    Phone = employeeDTO.Phone,
                    Position = employeeDTO.Position,
                    Salary = employeeDTO.Salary,
                    JoinDate = employeeDTO.JoinDate,
                    Status = employeeDTO.Status,
                    ManagerId = employeeDTO.ManagerId,
                    CreatedDate = DateTime.Now
                };

                var result = await _repository.AddAsync(employee);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating employee", ex);
            }
        }

        // Update employee
        public async Task<EmployeeDTO> UpdateEmployeeAsync(int id, EmployeeDTO employeeDTO)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid employee ID");

                if (employeeDTO == null)
                    throw new ArgumentNullException(nameof(employeeDTO));

                var employee = await _repository.GetByIdAsync(id);

                if (employee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found");

                // Update properties
                employee.FirstName = employeeDTO.FirstName;
                employee.LastName = employeeDTO.LastName;
                employee.Email = employeeDTO.Email;
                employee.Phone = employeeDTO.Phone;
                employee.Position = employeeDTO.Position;
                employee.Salary = employeeDTO.Salary;
                employee.Status = employeeDTO.Status;
                employee.ManagerId = employeeDTO.ManagerId;
                employee.UpdatedDate = DateTime.Now;

                var result = await _repository.UpdateAsync(employee);
                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee with ID {id}", ex);
            }
        }

        // Delete employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid employee ID");

                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee with ID {id}", ex);
            }
        }

        // Search employees
        public async Task<IEnumerable<EmployeeDTO>> SearchEmployeesAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    throw new ArgumentException("Search term cannot be empty");

                var employees = await _repository.SearchAsync(searchTerm);
                return employees.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees", ex);
            }
        }

        // Get employees by status
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByStatusAsync(int statusId)
        {
            try
            {
                var employees = await _repository.GetByStatusAsync(statusId);
                return employees.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving employees by status", ex);
            }
        }

        // Helper method: Map Entity to DTO
        private EmployeeDTO MapToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Position = employee.Position,
                Salary = employee.Salary,
                JoinDate = employee.JoinDate,
                Status = employee.Status,
                ManagerId = employee.ManagerId,
                CreatedDate = employee.CreatedDate
            };
        }
    }
}