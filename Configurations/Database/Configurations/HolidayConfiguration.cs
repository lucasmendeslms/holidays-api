using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        private const int MAX_NAME_LENGTH = 200;

        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holidays");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(MAX_NAME_LENGTH).IsRequired();
            builder.Property(p => p.Day).IsRequired();
            builder.Property(p => p.Month).IsRequired();
            builder.Property(p => p.Type);
        }
    }
}