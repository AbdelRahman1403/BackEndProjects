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
    internal class HealthRecordConfigurations : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Member")
                   .HasKey(hr => hr.Id);

            builder.HasOne<Member>()
                   .WithOne()
                   .HasForeignKey<HealthRecord>(hr => hr.Id);
            builder.Ignore(up => up.UpdatedAt);
            builder.Ignore(cr => cr.CreatedAt);
        }
    }
}
