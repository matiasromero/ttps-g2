using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class AppliedVaccineEntityTypeConfiguration : IEntityTypeConfiguration<AppliedVaccine>
    {
        public void Configure(EntityTypeBuilder<AppliedVaccine> builder)
        {
            builder.ToTable("AppliedVaccines");
            builder.HasOne(av => av.User).WithMany(u => u.Vaccines).HasForeignKey(av => av.UserId).IsRequired();
            builder.HasOne(av => av.LocalBatchVaccine);
            builder.HasOne(av => av.Patient).WithMany(p => p.AppliedVaccines).HasForeignKey(av => av.PatientId).IsRequired();
        }
    }
}
