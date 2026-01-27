using System.Text.Json;

using EmployeeManager.Models;

using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Data;

public class EmployeeSeederService
{
    private readonly AppDbContext _context;
    private readonly ILogger<EmployeeSeederService> _logger;

    public EmployeeSeederService(AppDbContext context, ILogger<EmployeeSeederService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedEmployeesFromJsonAsync(string jsonFilePath)
    {
        try
        {
            if (!File.Exists(jsonFilePath))
            {
                _logger.LogError("Employee seed file not found at: {FilePath}", jsonFilePath);
                throw new FileNotFoundException($"Employee seed file not found at: {jsonFilePath}");
            }

            _logger.LogInformation("Reading employee seed data from: {FilePath}", jsonFilePath);
            var jsonContent = await File.ReadAllTextAsync(jsonFilePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var employeeDtos = JsonSerializer.Deserialize<List<EmployeeSeedDto>>(jsonContent, options);

            if (employeeDtos == null || employeeDtos.Count == 0)
            {
                _logger.LogWarning("No employees found in seed file");
                return;
            }

            _logger.LogInformation("Processing {Count} employees from seed file", employeeDtos.Count);

            // Track ID mapping for superior relationships
            var idMapping = new Dictionary<int, int>();

            // First pass: Create all employees and addresses (without superior relationships)
            foreach (var dto in employeeDtos)
            {
                var oldId = employeeDtos.IndexOf(dto) + 1;

                // Create address
                var address = new Address
                {
                    Street = dto.Address.Street,
                    ZipCode = dto.Address.ZipCode,
                    City = dto.Address.City,
                    CountryId = dto.Address.CountryId
                };

                _context.Add(address);
                await _context.SaveChangesAsync();

                // Create employee
                var employee = new Employee
                {
                    FirstName = dto.FirstName,
                    MiddleName = dto.MiddleName,
                    LastName = dto.LastName,
                    BirthDate = DateTime.Parse(dto.BirthDate),
                    Gender = Enum.Parse<Gender>(dto.Gender),
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    JoinedDate = DateTime.Parse(dto.JoinedDate),
                    ExitedDate = !string.IsNullOrEmpty(dto.ExitedDate) ? DateTime.Parse(dto.ExitedDate) : null,
                    AddressId = address.Id,
                    CountryId = dto.CountryId,
                    SuperiorId = null // Set in second pass
                };

                _context.Add(employee);
                await _context.SaveChangesAsync();

                idMapping[oldId] = employee.Id;

                // Add salaries
                if (dto.Salaries != null)
                {
                    foreach (var salaryDto in dto.Salaries)
                    {
                        var salary = new Salary
                        {
                            Amount = salaryDto.Amount,
                            From = DateTime.Parse(salaryDto.From),
                            To = !string.IsNullOrEmpty(salaryDto.To) ? DateTime.Parse(salaryDto.To) : null,
                            EmployeeId = employee.Id
                        };
                        _context.Add(salary);
                    }
                }

                // Add job categories
                if (dto.JobCategoryIds != null)
                {
                    foreach (var jobCategoryId in dto.JobCategoryIds)
                    {
                        var employeeJobCategory = new EmployeeJobCategory
                        {
                            EmployeeId = employee.Id,
                            JobCategoryId = jobCategoryId
                        };
                        _context.Add(employeeJobCategory);
                    }
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("Created employee: {FirstName} {LastName} (ID: {Id})",
                    employee.FirstName, employee.LastName, employee.Id);
            }

            // Second pass: Update superior relationships
            for (int i = 0; i < employeeDtos.Count; i++)
            {
                var dto = employeeDtos[i];
                if (dto.SuperiorId.HasValue)
                {
                    var oldId = i + 1;
                    var newEmployeeId = idMapping[oldId];
                    var newSuperiorId = idMapping[dto.SuperiorId.Value];

                    var employee = await _context.Employees.FindAsync(newEmployeeId);
                    if (employee != null)
                    {
                        employee.SuperiorId = newSuperiorId;
                        _context.Update(employee);
                    }
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully seeded {Count} employees with their relationships", employeeDtos.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while seeding employees");
            throw;
        }
    }

    // DTOs for JSON deserialization
    private class EmployeeSeedDto
    {
        public string FirstName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = default!;
        public string BirthDate { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string JoinedDate { get; set; } = default!;
        public string? ExitedDate { get; set; }
        public int? SuperiorId { get; set; }
        public AddressSeedDto Address { get; set; } = default!;
        public int CountryId { get; set; }
        public List<SalarySeedDto>? Salaries { get; set; }
        public List<int>? JobCategoryIds { get; set; }
    }

    private class AddressSeedDto
    {
        public string Street { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public int CountryId { get; set; }
    }

    private class SalarySeedDto
    {
        public decimal Amount { get; set; }
        public string From { get; set; } = default!;
        public string? To { get; set; }
    }
}
