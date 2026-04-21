using System.ComponentModel.DataAnnotations;

namespace Shopee.EmployeeCRM.Shared.Dtos;

public class ClientDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ContactPerson { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    [StringLength(250)]
    public string Notes { get; set; } = string.Empty;
}
