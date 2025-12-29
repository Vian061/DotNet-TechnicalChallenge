using AutoMapper;
using BuildingBlocks.Common.Extentions;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Application.MappingProfiles
{
    public class DoctorScheduleProfiles : Profile
    {
        public DoctorScheduleProfiles()
        {
            CreateMap<DoctorScheduleDTO, DoctorSchedule>()
                .ForMember(dest => dest.DaysOfWeek, opt => opt.MapFrom(src => src.DaysOfWeek.ToEnumOrDefault<DayOfWeekFlags>()) );

            CreateMap<CreateDoctorScheduleDTO, DoctorSchedule>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.DoctorId))
                .ForMember(dest => dest.DaysOfWeek, opt => opt.MapFrom(src => src.DaysOfWeek.ToEnumOrDefault<DayOfWeekFlags>()) );

            CreateMap<DoctorSchedule, DoctorScheduleDTO>()
                .ForMember(dest => dest.DaysOfWeek, opt => opt.MapFrom(src => src.DaysOfWeek.ToString()));
        }
    }
}
