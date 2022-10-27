using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class BatchVaccineEntityTypeConfiguration : IEntityTypeConfiguration<BatchVaccine>
    {
        public void Configure(EntityTypeBuilder<BatchVaccine> builder)
        {
            builder.ToTable("BatchVaccines");
            builder.HasOne(b => b.DevelopedVaccine).WithMany().IsRequired();
            // builder.HasOne(b => b.PurchaseOrder).U.WithMany().IsRequired();
            builder.HasIndex(b => b.BatchNumber).IsUnique();
            builder.Property(b => b.BatchNumber).HasField("_batchNumber").HasMaxLength(20).IsRequired();
        }
    }
}
