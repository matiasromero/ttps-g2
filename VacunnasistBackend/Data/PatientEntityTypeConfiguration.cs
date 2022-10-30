using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VacunassistBackend.Entities;
using VacunnasistBackend.Entities;

namespace VacunnasistBackend.Data
{
    public class PatientEntityTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Surname).HasMaxLength(100).IsRequired();
            builder.Property(b => b.DNI).HasMaxLength(20).IsRequired();
            builder.Property(b => b.Province).IsRequired();
            builder.Property(b => b.BirthDate).IsRequired();
            builder.Property(b => b.Gender).IsRequired();
            builder.HasMany(p => p.AppliedVaccines).WithOne();
        }
    }    
}
