using System.ComponentModel.DataAnnotations;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Shared.Dtos;

public class EmployeeDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Designation { get; set; } = string.Empty;

    public EmployeeRole Role { get; set; } = EmployeeRole.Employee;

    public DateTime JoinedOn { get; set; } = DateTime.Today;

    public int AssignedClientsCount { get; set; }

    public int CompletedTasksCount { get; set; }
}
