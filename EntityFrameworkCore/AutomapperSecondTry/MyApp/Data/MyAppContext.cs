namespace MyApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using MyApp.Models;


    public class MyAppContext : DbContext
    {
        public MyAppContext()
        {
        }
        public MyAppContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEmployee(modelBuilder);
        }

        private void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(x =>
            {
                x.HasKey(e => e.Id);

                x.Property(e => e.FirstName).IsRequired();

                x.Property(e => e.LastName).IsRequired();

                x.Property(e => e.Salary).IsRequired();
            });                 
        }
    }
}
