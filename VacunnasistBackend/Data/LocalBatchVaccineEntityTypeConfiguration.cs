using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class LocalBatchVaccineEntityTypeConfiguration : IEntityTypeConfiguration<LocalBatchVaccine>
    {
        public void Configure(EntityTypeBuilder<LocalBatchVaccine> builder)
        {
            builder.ToTable("LocalBatchVaccines");
            builder.HasOne(av => av.BatchVaccine).WithMany(u => u.Distributions).HasForeignKey(av => av.BatchVaccineId).IsRequired();
            builder.Property(b => b.Province).HasMaxLength(50).IsRequired();
        }
    }
}
