using Microsoft.EntityFrameworkCore;
using HolidayApi.Data.Entities;

namespace HolidayApi.Configurations.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<State> State { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}