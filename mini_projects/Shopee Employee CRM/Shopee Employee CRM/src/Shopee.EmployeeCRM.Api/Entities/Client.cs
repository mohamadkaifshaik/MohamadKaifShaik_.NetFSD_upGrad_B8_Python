using System.ComponentModel.DataAnnotations;

namespace Shopee.EmployeeCRM.Api.Entities;

public class Client
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ContactPerson { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Notes { get; set; } = string.Empty;

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}
