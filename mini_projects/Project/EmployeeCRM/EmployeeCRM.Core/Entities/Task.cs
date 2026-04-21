using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeCRM.Core.Enums;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.Core.Entities
{
    /// <summary>
    /// Represents a task assigned to an employee
    /// Note: Named "TaskItem" to avoid conflict with System.Threading.Tasks
    /// </summary>
    public class TaskItem
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        // Current status of the task
        public TaskStatusEnum Status { get; set; }

        // Priority (1 = Low, 2 = Medium, 3 = High)
        public int Priority { get; set; }

        public DateTime DueDate { get; set; }

        // Who the task is assigned to
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // Which client this task is related to (optional)
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        // Progress percentage (0-100)
        public int ProgressPercentage { get; set; }

        // Tracking
        public DateTime CreatedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
