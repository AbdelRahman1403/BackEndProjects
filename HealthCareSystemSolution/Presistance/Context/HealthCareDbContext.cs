using ApplicationLayer.Entities;
using ApplicationLayer.Entities.AppointmentsModels;
using ApplicationLayer.Entities.MedicalStuffModels;
using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using ApplicationLayer.Entities.PatientModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class HealthCareDbContext : IdentityDbContext<ApplicationUser , IdentityRole<int> , int>
    {
        public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<MedicalSpecialty> MedicalSpecialties => Set<MedicalSpecialty>();
        public DbSet<ReservationTime> ReservationTimes => Set<ReservationTime>();
        public DbSet<MedicalPhoneNumbers> MedicalPhoneNumbers => Set<MedicalPhoneNumbers>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Allergy> Allergies => Set<Allergy>();
        public DbSet<PatientAllergy> PatientAllergies => Set<PatientAllergy>();
        public DbSet<PhoneNumbers> PatientPhoneNumbers => Set<PhoneNumbers>();
    }
}
