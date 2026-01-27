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
        var employees = await _context.Employees.ToListAsync();
        return _mapper.Map<List<EmployeeGetDto>>(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeGetDto>> GetById(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return NotFound();
        return _mapper.Map<EmployeeGetDto>(employee);
    }
}
