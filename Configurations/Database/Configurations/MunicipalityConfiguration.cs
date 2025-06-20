using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.ToTable("Municipalities");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.IbgeCode).IsRequired();
            builder.HasIndex(p => p.IbgeCode).IsUnique();
            builder.HasMany(p => p.Holidays).WithOne(h => h.Municipality).HasForeignKey(h => h.MunicipalityId).IsRequired(false);

            builder.HasIndex(p => p.IbgeCode);
        }
    }
}