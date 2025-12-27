using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configurations
{
    internal class GymUserConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
                                                 where TEntity : GymUser
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(em => em.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(nm => nm.Name).HasColumnType("varchar").HasMaxLength(50);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserEmailChick", "Email LIKE '%_@__%.__%'");
                tb.HasCheckConstraint("GymUserNameChick", "LEN(Name) >= 2");
                tb.HasCheckConstraint("GymUserPhoneChick", "Phone LIKE 010________");
            });

            builder.HasIndex(em => em.Email).IsUnique();
            builder.HasIndex(ph => ph.Phone).IsUnique();

            builder.OwnsOne(ad => ad.address, UserAddress =>
            {
                UserAddress.Property(st => st.Street)
                           .HasColumnType("varchar")
                           .HasMaxLength(100)
                           .HasColumnName("Street");

                UserAddress.Property(ct => ct.City)
                           .HasColumnType("varchar")
                           .HasMaxLength(50)
                           .HasColumnName("City");

                UserAddress.Property(bdnum => bdnum.BuildingNumber)
                           .HasColumnName("BuildingNumber");
            });
        }
    }
}
