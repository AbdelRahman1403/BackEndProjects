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
    public class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            builder.Property(n => n.ShortName).HasColumnType("varchar(50)");
            builder.Property(n => n.Descripiton).HasColumnType("varchar(50)");
            builder.Property(n => n.DeliveryTime).HasColumnType("varchar(20)");

            builder.Property(p => p.Price).HasColumnType("decimal(8,2)");
        }
    }
}
