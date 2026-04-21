using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;

namespace EmployeeCRM.MVC.Services
{
    public interface IDashboardApiClient
    {
        Task<DashboardDTO> GetMetricsAsync(string token);
    }
}