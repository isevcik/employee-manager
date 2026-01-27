using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Models;

public class Employee
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = default!;

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required, MaxLength(100)]
    public string LastName { get; set; } = default!;

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address Address { get; set; } = default!;

    [ForeignKey("Country")]
    public int CountryId { get; set; }
    public Country Country { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Phone]
    public string? PhoneNumber { get; set; }

    public DateTime JoinedDate { get; set; }
    public DateTime? ExitedDate { get; set; }

    [ForeignKey("Superior")]
    public int? SuperiorId { get; set; }
    public Employee? Superior { get; set; }

    public List<Employee> Subordinates { get; set; } = new();

    public List<Salary> Salaries { get; set; } = new();
    
    public List<EmployeeJobCategory> EmployeeJobCategories { get; set; } = new();
}

