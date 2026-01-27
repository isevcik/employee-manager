using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models;

public class EmployeeJobCategory
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;

    public int JobCategoryId { get; set; }
    public JobCategory JobCategory { get; set; } = default!;
}
