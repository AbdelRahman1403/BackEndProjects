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
    public class ReservationTimeConfigurations : IEntityTypeConfiguration<ReservationTime>
    {
        public void Configure(EntityTypeBuilder<ReservationTime> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Capacity).IsRequired();

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_ReservationTime_Capacity", "[Capacity] between 1 and 25")
            );

            builder.HasMany(b => b.Appointments)
                .WithOne(a => a.AppointmentDateTime)
                .HasForeignKey(a => a.ReservationTimeId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
