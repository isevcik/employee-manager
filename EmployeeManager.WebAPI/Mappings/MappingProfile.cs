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
        CreateMap<Address, AddressDto>();
        CreateMap<Salary, SalaryDto>();
        CreateMap<Employee, EmployeeSummaryDto>();
        CreateMap<Employee, EmployeeDetailDto>()
            .ForMember(dest => dest.JobCategories,
                opt => opt.MapFrom(src => src.EmployeeJobCategories.Select(ejc => ejc.JobCategory)))
            .ForMember(dest => dest.Salary,
                opt => opt.MapFrom(src => src.Salaries.FirstOrDefault(s => s.To == null)));
        CreateMap<Employee, EmployeeListDto>();
    }
}
