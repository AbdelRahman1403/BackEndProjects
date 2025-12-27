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
    internal class TrainerConfigurations : GymUserConfiguration<Trainer> , IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(d => d.CreatedAt)
                   .HasColumnName("HiringDate")
                   .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);
        }
    }
}
