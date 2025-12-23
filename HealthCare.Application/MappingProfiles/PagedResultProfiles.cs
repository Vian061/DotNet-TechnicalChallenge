using AutoMapper;
using BuildingBlocks.Shared.Entities;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;

namespace HealthCare.Application.MappingProfiles
{
    public class PagedResultProfiles : Profile
    {
        public PagedResultProfiles()
        {
            CreateMap<PagedResult<Appointment>, PagedResult<AppointmentDTO>>();
            CreateMap<PagedResult<Doctor>, PagedResult<DoctorDTO>>();
            CreateMap<PagedResult<DoctorSchedule>, PagedResult<DoctorScheduleDTO>>();
            CreateMap<PagedResult<Patient>, PagedResult<PatientDTO>>();
        }
    }
}
