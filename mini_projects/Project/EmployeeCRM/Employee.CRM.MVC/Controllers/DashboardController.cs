using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.MVC.Services;

namespace EmployeeCRM.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardApiClient _dashboardApiClient;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardApiClient dashboardApiClient, ILogger<DashboardController> logger)
        {
            _dashboardApiClient = dashboardApiClient;
            _logger = logger;
        }

        // Dashboard home page with metrics
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var metrics = await _dashboardApiClient.GetMetricsAsync(token);

                return View(metrics);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading dashboard metrics: {ex.Message}");
                TempData["Error"] = "Error loading dashboard metrics";
                return View(new DashboardDTO());
            }
        }
    }
}