using AutoMapper;
using Demo.BLL.DTOS;
using Demo.BLL.DTOS.DepartmentDTOS;
using Demo.BLL.DTOS.EmployeeDTOS;
using Demo.DAL.Models.DepartmentModule;
using Demo.DAL.Models.EmployeeModule;

namespace Demo.BLL.Mappings;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
        CreateMap<Employee , EmployeeDetailsDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
        CreateMap<CreateEmployeeDto, Employee>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
    }
}