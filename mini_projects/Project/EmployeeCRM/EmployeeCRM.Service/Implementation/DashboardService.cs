using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Enums;
using EmployeeCRM.Core.Interfaces;
using TaskStatus = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.Service.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITaskRepository _taskRepository;

        public DashboardService(
            IEmployeeRepository employeeRepository,
            IClientRepository clientRepository,
            ITaskRepository taskRepository)
        {
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _taskRepository = taskRepository;
        }

        // Get dashboard metrics
        public async Task<DashboardDTO> GetDashboardMetricsAsync()
        {
            try
            {
                // Get all data
                var employees = await _employeeRepository.GetAllAsync();
                var clients = await _clientRepository.GetAllAsync();
                var tasks = await _taskRepository.GetAllAsync();

                // Count total records
                var totalEmployees = employees.Count();
                var totalClients = clients.Count();
                var totalTasks = tasks.Count();

                // Count task statuses
                var completedTasks = tasks.Count(t => t.Status == TaskStatus.Completed);
                var pendingTasks = tasks.Count(t => t.Status == TaskStatus.Pending);

                // Calculate total salaries
                var totalSalaries = employees.Sum(e => e.Salary);

                return new DashboardDTO
                {
                    TotalEmployees = totalEmployees,
                    TotalClients = totalClients,
                    TotalTasks = totalTasks,
                    CompletedTasks = completedTasks,
                    PendingTasks = pendingTasks,
                    TotalSalaries = totalSalaries
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving dashboard metrics", ex);
            }
        }
    }
}