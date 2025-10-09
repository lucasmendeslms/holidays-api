using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        private const int MAX_NAME_LENGTH = 200;
        private const int MAX_STATE_CODE_LENGTH = 2;

        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(MAX_NAME_LENGTH).IsRequired();
            builder.Property(p => p.StateCode).HasMaxLength(MAX_STATE_CODE_LENGTH).IsRequired();
            builder.Property(p => p.IbgeCode).IsRequired();
            builder.HasIndex(p => p.IbgeCode).IsUnique();
            builder.HasMany(p => p.Municipalities).WithOne(m => m.State).HasForeignKey(m => m.StateId);
            builder.HasMany(p => p.Holidays).WithOne(h => h.State).HasForeignKey(h => h.StateId).IsRequired(false);

            builder.HasIndex(p => p.IbgeCode);
        }
    }
}


//https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration

//https://learn.microsoft.com/pt-br/ef/core/modeling/relationships/one-to-many