using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class LaboratoryEntityTypeConfiguration : IEntityTypeConfiguration<Laboratory>
    {
        public void Configure(EntityTypeBuilder<Laboratory> builder)
        {
            builder.ToTable("Laboratories");
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
        }
    }
}
