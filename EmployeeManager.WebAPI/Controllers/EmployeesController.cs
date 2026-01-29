using AutoMapper;

using EmployeeManager.Data;
using EmployeeManager.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EmployeesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeGetDto>>> GetAll()
    {
        var employees = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.EmployeeJobCategories)
                .ThenInclude(ejc => ejc.JobCategory)
            .Include(e => e.Salaries)
            .ToListAsync();
        return _mapper.Map<List<EmployeeGetDto>>(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeGetDto>> GetById(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.EmployeeJobCategories)
                .ThenInclude(ejc => ejc.JobCategory)
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return NotFound();
        return _mapper.Map<EmployeeGetDto>(employee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");

        var employee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.Salaries)
            .Include(e => e.EmployeeJobCategories)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null) return NotFound();

        // Update basic properties
        employee.FirstName = dto.FirstName;
        employee.MiddleName = dto.MiddleName;
        employee.LastName = dto.LastName;
        employee.BirthDate = dto.BirthDate;
        employee.Gender = Enum.Parse<Models.Gender>(dto.Gender);
        // employee.CountryId = dto.CountryId;
        employee.Email = dto.Email;
        employee.PhoneNumber = dto.PhoneNumber;
        employee.JoinedDate = dto.JoinedDate;
        employee.ExitedDate = dto.ExitedDate;
        employee.SuperiorId = dto.SuperiorId;

        // Update Address
        if (dto.Address != null)
        {
            if (employee.Address == null)
            {
                employee.Address = _mapper.Map<Models.Address>(dto.Address);
            }
            else
            {
                employee.Address.Street = dto.Address.Street;
                employee.Address.City = dto.Address.City;
                employee.Address.ZipCode = dto.Address.ZipCode;
                employee.Address.CountryId = dto.Address.CountryId;
            }
        }

        // Handle salary update
        var latestSalary = employee.Salaries
            .Where(s => s.To == null)
            .OrderByDescending(s => s.From)
            .FirstOrDefault();

        if (latestSalary == null || latestSalary.Amount != dto.Salary)
        {
            // Close the current salary record
            if (latestSalary != null)
            {
                latestSalary.To = DateTime.Today;
            }

            // Create new salary record
            var newSalary = new Models.Salary
            {
                Amount = dto.Salary,
                From = DateTime.Today,
                To = null,
                EmployeeId = employee.Id
            };
            employee.Salaries.Add(newSalary);
        }

        // Update job categories
        employee.EmployeeJobCategories.Clear();
        foreach (var jobCategoryDto in dto.JobCategories)
        {
            employee.EmployeeJobCategories.Add(new Models.EmployeeJobCategory
            {
                EmployeeId = employee.Id,
                JobCategoryId = jobCategoryDto.Id
            });
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeGetDto>> Create(EmployeeUpdateDto dto)
    {
        var employee = new Models.Employee
        {
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            BirthDate = dto.BirthDate,
            Gender = Enum.Parse<Models.Gender>(dto.Gender),
            // CountryId = dto.CountryId,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            JoinedDate = dto.JoinedDate,
            ExitedDate = dto.ExitedDate,
            SuperiorId = dto.SuperiorId
        };

        // Add Address if provided
        if (dto.Address != null)
        {
            employee.Address = _mapper.Map<Models.Address>(dto.Address);
        }

        // Add initial salary
        employee.Salaries.Add(new Models.Salary
        {
            Amount = dto.Salary,
            From = DateTime.Today,
            To = null
        });

        // Add job categories
        foreach (var jobCategoryDto in dto.JobCategories)
        {
            employee.EmployeeJobCategories.Add(new Models.EmployeeJobCategory
            {
                JobCategoryId = jobCategoryDto.Id
            });
        }

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var createdEmployee = await _context.Employees
            .Include(e => e.Address)
            .Include(e => e.EmployeeJobCategories)
                .ThenInclude(ejc => ejc.JobCategory)
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == employee.Id);

        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, _mapper.Map<EmployeeGetDto>(createdEmployee));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
