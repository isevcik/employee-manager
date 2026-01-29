namespace EmployeeManager.DTOs;

public class EmployeeSummaryDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}
