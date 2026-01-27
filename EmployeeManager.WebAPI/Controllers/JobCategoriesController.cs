using AutoMapper;
using EmployeeManager.Data;
using EmployeeManager.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobCategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public JobCategoriesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobCategoryDto>>> GetAll()
    {
        var jobCategories = await _context.JobCategories.ToListAsync();
        return _mapper.Map<List<JobCategoryDto>>(jobCategories);
    }
}
