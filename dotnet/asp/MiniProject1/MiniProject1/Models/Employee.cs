using System.ComponentModel.DataAnnotations;

public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Email { get; set; }

    public string Department { get; set; }

    public int ExperienceYears { get; set; }

    public ICollection<Client> Clients { get; set; }
}
