using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.Core.Interfaces;

namespace EmployeeCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("metrics")]
        [ProducesResponseType(typeof(DashboardDTO), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetDashboardMetrics()
        {
            try
            {
                var metrics = await _dashboardService.GetDashboardMetricsAsync();
                return Ok(metrics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving dashboard metrics", error = ex.Message });
            }
        }
    }
}