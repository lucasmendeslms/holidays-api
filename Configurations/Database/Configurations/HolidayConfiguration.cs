using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holidays");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Day).IsRequired();
            builder.Property(p => p.Month).IsRequired();
            builder.Property(p => p.Year);
            builder.HasMany(p => p.States)
                .WithMany(p => p.Holidays)
                .UsingEntity<Dictionary<int, object>>(
                    "HolidayState",
                    s => s
                    .HasOne<State>()
                    .WithMany()
                    .HasForeignKey("StateCode")
                    .HasPrincipalKey(s => s.IbgeCode)
                );
            builder.HasMany(p => p.Municipalities)
                .WithMany(p => p.Holidays)
                .UsingEntity<Dictionary<int, object>>(
                    "HolidayMunicipality",
                    m => m.HasOne<Municipality>()
                    .WithMany()
                    .HasForeignKey("MunicipalityCode")
                    .HasPrincipalKey(m => m.IbgeCode)
                );
        }
    }
}