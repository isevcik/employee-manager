using AutoMapper;
using EmployeeManager.DTOs;
using EmployeeManager.Models;

namespace EmployeeManager.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDto>();
        CreateMap<JobCategory, JobCategoryDto>();
        CreateMap<Employee, EmployeeGetDto>();
    }
}
