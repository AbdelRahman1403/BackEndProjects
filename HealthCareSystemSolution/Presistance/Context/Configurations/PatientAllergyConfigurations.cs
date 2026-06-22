using ApplicationLayer.Entities.PatientModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context.Configurations
{
    public class PatientAllergyConfigurations : IEntityTypeConfiguration<PatientAllergy>
    {
        public void Configure(EntityTypeBuilder<PatientAllergy> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.HasOne(pa => pa.Patient)
                   .WithMany(p => p.PatientAllergy)
                   .HasForeignKey(pa => pa.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pa => pa.Allergy)
                   .WithMany(a => a.PatientAllergies)
                   .HasForeignKey(pa => pa.AllergyId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(pa => pa.Severity)
                   .HasColumnType("decimal(3,2)")
                   .IsRequired();
        }
    }
}
