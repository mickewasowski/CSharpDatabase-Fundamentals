using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureProductEntity(modelBuilder);

            ConfigureCustomerEntity(modelBuilder);

            ConfigureStoreEntity(modelBuilder);

            ConfigureSaleEntity(modelBuilder);
        }

        private void ConfigureSaleEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Sale>()
                .HasKey(s => s.SaleId);
        }

        private void ConfigureStoreEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Store>()
                .HasKey(s => s.StoreId);

            modelBuilder
                .Entity<Store>()
                .HasMany(st => st.Sales)
                .WithOne(s => s.Store);

            modelBuilder
                .Entity<Store>()
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();
        }

        private void ConfigureCustomerEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder
              .Entity<Customer>()
              .HasMany(c => c.Sales)
              .WithOne(s => s.Customer);

            modelBuilder
                .Entity<Customer>()
                .Property(n => n.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Customer>()
                .Property(n => n.Email)
                .HasMaxLength(80);
        }

        private void ConfigureProductEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder
                .Entity<Product>()
                .HasMany(p => p.Sales)
                .WithOne(s => s.Product);

            modelBuilder
                .Entity<Product>()
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Product>()
                .Property(d => d.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");
        }
    }
}
