using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.MappingProfiles
{
    public class DoctorProfiles : Profile
    {
        public DoctorProfiles()
        {
            CreateMap<DoctorDTO, Doctor>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Alias) ? Guid.NewGuid().ToString() : src.Alias));
            CreateMap<CreateDoctorDTO, Doctor>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Alias) ? Guid.NewGuid().ToString() : src.Alias));
            CreateMap<Doctor, DoctorDTO>();
		}
    }
}
