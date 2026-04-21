using EmployeeCRM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Entities
{
    /// <summary>
    /// Represents a system user (Admin, Manager, Employee)
    /// Used for authentication and authorization
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PasswordHash { get; set; }
        // User role: Admin, Manager, Employee
        public UserRole Role { get; set; }

        // Account status
        public bool IsActive { get; set; }

        // Tracking properties
        public DateTime CreatedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        // Navigation property - A user can be an employee
        public Employee? Employee { get; set; }
    }
}
