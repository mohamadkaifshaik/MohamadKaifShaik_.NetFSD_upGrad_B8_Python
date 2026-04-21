using Microsoft.AspNetCore.Mvc;
using Shopee.EmployeeCRM.Api.Services;
using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
    {
        var summary = await dashboardService.GetSummaryAsync();
        return Ok(summary);
    }
}
