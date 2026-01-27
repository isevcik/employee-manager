using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models;

public class JobCategory
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = default!;
    
    public List<EmployeeJobCategory> EmployeeJobCategories { get; set; } = new();
}
