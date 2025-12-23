using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.MappingProfiles
{
    public class DoctorProfiles : Profile
    {
        public DoctorProfiles()
        {
            CreateMap<DoctorDTO, Doctor>().ReverseMap();
            CreateMap<CreateDoctorDTO, Doctor>()
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.TimeZone) ? "Asia/Jakarta" : src.TimeZone))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
