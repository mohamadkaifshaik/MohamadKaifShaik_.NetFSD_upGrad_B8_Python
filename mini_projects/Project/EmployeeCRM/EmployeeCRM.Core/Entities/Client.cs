using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Entities
{
    /// <summary>
    /// Represents a client/customer
    /// </summary>
    public class Client
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; }

        public string? ContactName { get; set; }

        public string? Email { get; set; }      
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }

        public string? ZipCode { get; set; }

        // Rating (1-5)
        public int Rating { get; set; }

        // Employee managing this client
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // Tasks associated with this client
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        // Tracking
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
