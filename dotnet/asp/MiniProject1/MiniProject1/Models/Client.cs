using System.ComponentModel.DataAnnotations;

public class Client
{
    public int Id { get; set; }

    [Required]
    public string ClientName { get; set; }

    public string ProjectName { get; set; }

    public decimal ProjectValue { get; set; }

    // Foreign Key
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
