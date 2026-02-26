using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.Store.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.Property(p => p.Name).HasColumnName("varchar(50)");
            builder.Property(p => p.Description).HasColumnName("varchar(500)");
            builder.Property(p => p.PictureUrl).HasColumnName("varchar(250)");
            builder.Property(p => p.Price).HasColumnName("decimal(10,2)");

            builder.HasOne(b => b.productBrand)
                   .WithMany()
                   .HasForeignKey(p => p.BrandId);

            builder.HasOne(b => b.productType)
                   .WithMany()
                   .HasForeignKey(p => p.TypeId);


        }
    }
}
