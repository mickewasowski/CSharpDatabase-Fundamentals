using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using System;
namespace MyApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext()
        {

        }
        public MyAppContext(DbContextOptions options) 
            : base(options)
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
