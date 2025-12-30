using HealthCare.Domain.Enums;
using HealthCare.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HealthCare.Application.Services
{
    public class WeeklyDoctorSchedule
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;

        public WeeklyDoctorSchedule(IDoctorScheduleRepository doctorScheduleRepository)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
        }

        public async Task Run(int doctorId, DayOfWeekFlags daysOfWeek, TimeSpan StartTime, TimeSpan EndTime)
        {
            var doctorsSchedules = _doctorScheduleRepository.GetAllByDoctorIdAsync(doctorId);

            Trace.WriteLine(doctorsSchedules);
        }
    }
}
