using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class VaccineEntityTypeConfiguration : IEntityTypeConfiguration<DevelopedVaccine>
    {
        public void Configure(EntityTypeBuilder<DevelopedVaccine> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
        }
    }
}
