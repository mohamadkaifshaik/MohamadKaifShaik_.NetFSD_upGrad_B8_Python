using EmployeeCRM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.DTOs
{
    public class EmployeeDTO
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
        public int? ManagerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
