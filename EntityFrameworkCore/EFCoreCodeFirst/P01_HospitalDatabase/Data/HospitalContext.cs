using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; } 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePatientEntity(modelBuilder);

            ConfigureVisitationEntity(modelBuilder);

            ConfigureDiagnoseEntity(modelBuilder);

            ConfigureMedicamentEntity(modelBuilder);

            ConfigurePatientMedicamentEntity(modelBuilder);

            ConfigureDoctorEntity(modelBuilder);
        }

        private void ConfigureDoctorEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doctor>()
                .HasKey(d => d.DoctorId);

            modelBuilder
                .Entity<Doctor>()
                .HasMany(d => d.Visitations)
                .WithOne(v => v.Doctor);

            modelBuilder
                .Entity<Doctor>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Doctor>()
                .Property(p => p.Specialty)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();
        }

        private void ConfigurePatientMedicamentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(pm => new { pm.PatientId, pm.MedicamentId });

            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.PatientId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(m => m.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(m => m.MedicamentId);
        }

        private void ConfigureMedicamentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Medicament>()
                .HasKey(m => m.MedicamentId);

            modelBuilder
                .Entity<Medicament>()
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();
        }

        private void ConfigureDiagnoseEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Diagnose>()
                .HasKey(d => d.DiagnoseId);

            modelBuilder
                .Entity<Diagnose>()
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Diagnose>()
                .Property(c => c.Comments)
                .HasMaxLength(250)
                .IsUnicode()
                .IsRequired(); // todo

        }

        private void ConfigureVisitationEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Visitation>()
                .HasKey(v => v.VisitationId);

            modelBuilder
                 .Entity<Visitation>()
                 .Property(c => c.Comments)
                 .HasMaxLength(250)
                 .IsUnicode()
                 .IsRequired(); //todo
        }

        private void ConfigurePatientEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasKey(p => p.PatientId);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(v => v.Patient);

            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient);

            modelBuilder
                .Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
               .Entity<Patient>()
               .Property(p => p.LastName)
               .HasMaxLength(50)
               .IsUnicode()
               .IsRequired();

            modelBuilder
               .Entity<Patient>()
               .Property(p => p.Address)
               .HasMaxLength(250)
               .IsUnicode()
               .IsRequired();

            modelBuilder
               .Entity<Patient>()
               .Property(p => p.Email)
               .HasMaxLength(80)
               .IsRequired(); // todo
        }
    }
}
