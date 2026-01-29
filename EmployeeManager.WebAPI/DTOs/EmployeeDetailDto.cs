namespace EmployeeManager.DTOs;

public class EmployeeDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = default!;
    public AddressDto? Address { get; set; }
    public CountryDto Country { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public DateTime JoinedDate { get; set; }
    public DateTime? ExitedDate { get; set; }
    public EmployeeSummaryDto? Superior { get; set; }
    public List<EmployeeSummaryDto> Subordinates { get; set; } = new();
    public SalaryDto? Salary { get; set; }
    public List<SalaryDto> Salaries { get; set; } = new();
    public List<JobCategoryDto> JobCategories { get; set; } = new();
}
