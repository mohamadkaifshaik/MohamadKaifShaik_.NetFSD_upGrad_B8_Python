using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatusEnum = EmployeeCRM.Core.Enums.TaskStatus;

namespace EmployeeCRM.Core.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskStatusEnum Status { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public int ProgressPercentage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
