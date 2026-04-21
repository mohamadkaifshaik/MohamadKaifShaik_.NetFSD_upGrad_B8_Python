using System.ComponentModel.DataAnnotations;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Shared.Dtos;

public class WorkTaskDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    [Required]
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);

    public WorkStatus Status { get; set; } = WorkStatus.Pending;
}
