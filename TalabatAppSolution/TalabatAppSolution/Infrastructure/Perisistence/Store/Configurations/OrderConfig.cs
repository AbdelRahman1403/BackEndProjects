using DomainLayer.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.Store.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(p => p.SubTotal).HasColumnType("decimal(8,2)");
            builder.Property(e => e.UserEmail).HasColumnType("varchar(50)");

            builder.HasOne(d => d.deliveryMethod).WithMany().HasForeignKey(id => id.deliveryMethodId);

            builder.OwnsOne(it => it.Address);

        }
    }
}
