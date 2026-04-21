using System.ComponentModel.DataAnnotations;
using Shopee.EmployeeCRM.Shared.Enums;

namespace Shopee.EmployeeCRM.Api.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Designation { get; set; } = string.Empty;

    public EmployeeRole Role { get; set; } = EmployeeRole.Employee;

    public DateTime JoinedOn { get; set; } = DateTime.Today;

    public ICollection<Client> Clients { get; set; } = new List<Client>();

    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}
