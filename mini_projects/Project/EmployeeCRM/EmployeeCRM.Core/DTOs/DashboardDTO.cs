using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.DTOs
{
    public class DashboardDTO
    {
        public int TotalEmployees { get; set; }
        public int TotalClients { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public decimal TotalSalaries { get; set; }
    }
}
