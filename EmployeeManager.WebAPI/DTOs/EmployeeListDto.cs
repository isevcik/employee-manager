namespace EmployeeManager.DTOs;

public class EmployeeListDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime JoinedDate { get; set; }
}
