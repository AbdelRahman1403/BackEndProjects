using ApplicationLayer.Entities.PatientModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context.Configurations
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.applicationUser)
                   .WithOne(u => u.Patient)
                   .HasForeignKey<Patient>(p => p.applicationUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.bloodType)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasMany(p => p.PhoneNumbers)
                   .WithOne(pt => pt.Patient)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
