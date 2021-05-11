using Microsoft.EntityFrameworkCore;

namespace mvc.Models.Data
{
    public class SynelDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }

        public SynelDbContext(DbContextOptions options)
            : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasKey(w => w.ID).IsClustered(true);
            builder.Entity<Employee>().HasIndex(e => e.HomeEmail).IsUnique(true);
        }
    }
}
