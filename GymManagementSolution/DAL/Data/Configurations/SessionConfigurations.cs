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
    internal class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("SessionCapacityChick", "Capacity between 1 and 25");
                tb.HasCheckConstraint("SessionEndDateChick", "EndTime > StartTime");
            });

            builder.HasOne(Cat => Cat.Category)
                   .WithMany(s => s.Sessions)
                   .HasForeignKey(id => id.CategoryId);
            builder.HasOne(tr => tr.TrainerSession)
                   .WithMany(s => s.Session)
                   .HasForeignKey(id => id.TrainerId);
        }
    }
}
