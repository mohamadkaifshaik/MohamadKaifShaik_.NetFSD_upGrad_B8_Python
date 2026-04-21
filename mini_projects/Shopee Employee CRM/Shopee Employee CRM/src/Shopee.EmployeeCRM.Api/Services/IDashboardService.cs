using Shopee.EmployeeCRM.Shared.Dtos;

namespace Shopee.EmployeeCRM.Api.Services;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}
