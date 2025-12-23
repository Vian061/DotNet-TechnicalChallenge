using AutoMapper;
using BuildingBlocks.Common.Extentions;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.MappingProfiles
{
    public class AppointmentProfiles : Profile
    {
        public AppointmentProfiles()
        {
            CreateMap<AppointmentDTO, Appointment>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Alias) ? Guid.NewGuid().ToString() : src.Alias))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnumOrDefault<AppointmentStatus>()));
            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
