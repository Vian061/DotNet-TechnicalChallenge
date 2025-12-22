using BuildingBlocks.Persistence.DBContex;
using HealthCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HealthCare.Infrastructure.Context
{
    public class HealthCareDBContext : SQLServerBaseContext
    {
        public HealthCareDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Appointment>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Doctor)
                 .WithMany()
                 .HasForeignKey(x => x.DoctorId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Patient)
                 .WithMany()
                 .HasForeignKey(x => x.PatientId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.DoctorId, x.StartUtc, x.EndUtc })
                 .HasFilter("[Status] = 10"); // Active only
            });
        }
    }
}
