using HolidayApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolidayApi.Configurations.Database
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
            builder.Property(p => p.StateCode).HasMaxLength(2).IsRequired();
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