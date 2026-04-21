namespace Shopee.EmployeeCRM.Shared.Dtos;

public class DashboardSummaryDto
{
    public int TotalEmployees { get; set; }

    public int TotalClients { get; set; }

    public int TotalTasks { get; set; }

    public int CompletedTasks { get; set; }

    public List<EmployeeDto> TopEmployees { get; set; } = [];

    public List<WorkTaskDto> UpcomingTasks { get; set; } = [];
}
