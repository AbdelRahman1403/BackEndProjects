using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    internal class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(n => n.PlanName)
                   .HasColumnType("varchar")
                   .HasMaxLength(30);
            builder.Property(d => d.PlanDescription)
                   .HasColumnType("varchar")
                   .HasMaxLength(150);
            builder.Property(p => p.Price).HasPrecision(10, 2);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("DurationInDaysChick", "DurationInDays between 1 and 365");
            });
        }
    }
}
