using EmployeeCRM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
    }
}
