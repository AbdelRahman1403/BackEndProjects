using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.Configurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.applicationUser)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.applicationUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(d => d.Description).HasMaxLength(500).IsRequired();
            builder.Property(b => b.Evaluation).HasColumnType("decimal(3,2)").IsRequired();

            builder.HasOne(d => d.MedicalSpecialty)
                   .WithMany(dc => dc.Doctors)
                   .HasForeignKey(d => d.MedicalSpecialtyId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(builder => builder.ReservationTimes)
                   .WithOne(rt => rt.Doctor)
                   .HasForeignKey(rt => rt.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(builder => builder.MedicalPhoneNumbers)
                   .WithOne(mp => mp.Doctor)
                   .HasForeignKey(mp => mp.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
