using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.MappingProfiles
{
    public class PatientProfiles : Profile
    {
        public PatientProfiles()
        {
            CreateMap<PatientDTO, Patient>().ReverseMap();
            CreateMap<CreatePatientDTO, Patient>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
