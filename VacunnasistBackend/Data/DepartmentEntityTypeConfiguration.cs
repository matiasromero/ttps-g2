using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VacunassistBackend.Entities;

namespace VacunassistBackend.Data
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Code).HasMaxLength(20).IsRequired();
            builder.HasOne(b => b.Province).WithMany().OnDelete(DeleteBehavior.NoAction).IsRequired();
        }
    }
}
