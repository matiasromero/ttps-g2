using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class PurchaseOrderEntityTypeConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");
            builder.HasOne(b => b.DevelopedVaccine).WithMany().IsRequired();
            builder.HasIndex(b => b.BatchNumber).IsUnique();
            builder.Property(b => b.BatchNumber).HasField("_batchNumber").HasMaxLength(20).IsRequired();
        }
    }
}
