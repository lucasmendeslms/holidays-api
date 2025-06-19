using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.CountryCode).HasMaxLength(2).IsRequired();
            builder.Property(p => p.IbgeCode).IsRequired();
            builder.HasIndex(p => p.IbgeCode).IsUnique();
            // builder.HasMany(p => p.States).WithOne(s => s.Country).HasForeignKey(s => s.CountryId);
            builder.HasMany(p => p.Holidays).WithOne(h => h.Country).HasForeignKey(h => h.CountryId).IsRequired(false);

            builder.HasIndex(p => p.IbgeCode);
        }
    }
}