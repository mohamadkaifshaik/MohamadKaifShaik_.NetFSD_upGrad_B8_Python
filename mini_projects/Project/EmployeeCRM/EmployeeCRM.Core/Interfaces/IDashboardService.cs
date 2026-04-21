using EmployeeCRM.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRM.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardMetricsAsync();
    }
}
