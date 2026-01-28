namespace EmployeeManager.DTOs;

public class EmployeeUpdateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = default!;
    public AddressDto? Address { get; set; }
    public int CountryId { get; set; }
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public DateTime JoinedDate { get; set; }
    public DateTime? ExitedDate { get; set; }
    public int? SuperiorId { get; set; }
    public decimal Salary { get; set; }
    public List<JobCategoryDto> JobCategories { get; set; } = new();
}
