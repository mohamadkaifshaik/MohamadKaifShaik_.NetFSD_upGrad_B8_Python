using EmployeeCRM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Entities
{
    /// <summary>
    /// Represents an employee in the organization
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }  
        public string? Phone { get; set; }

        public string? Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoinDate { get; set; }

        public EmployeeStatus Status { get; set; }

        // Manager relationship - Optional
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        // Subordinates
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        // Link to User account
        public int? UserId { get; set; }
        public User? User { get; set; }

        // Clients managed by this employee
        public ICollection<Client> Clients { get; set; } = new List<Client>();

        // Tasks assigned to this employee
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        // Tracking
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
