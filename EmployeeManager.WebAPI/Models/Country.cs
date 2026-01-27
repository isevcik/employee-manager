using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models;

public class Country
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;
}
