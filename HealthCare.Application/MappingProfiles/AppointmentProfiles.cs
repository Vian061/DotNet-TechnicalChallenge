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
            CreateMap<CreateAppointmentDTO, Appointment>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.EndUtc, opt => opt.MapFrom(src => src.StartUtc.AddMinutes(src.Duration)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.Active));
            CreateMap<AppointmentDTO, Appointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnumOrDefault<AppointmentStatus>()));
            CreateMap<Appointment, AppointmentDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
