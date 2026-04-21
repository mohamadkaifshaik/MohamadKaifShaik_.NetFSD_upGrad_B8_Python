using System.ComponentModel.DataAnnotations;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Api.Entities;

public class WorkTask
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);

    public WorkStatus Status { get; set; } = WorkStatus.Pending;
}
