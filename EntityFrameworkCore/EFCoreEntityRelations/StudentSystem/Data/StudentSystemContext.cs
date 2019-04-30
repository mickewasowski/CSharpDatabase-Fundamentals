using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Config.ConnectionString); 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureStudentEntity(modelBuilder);

            ConfigureCourseEntity(modelBuilder);

            ConfigureResourceEntity(modelBuilder);

            ConfigureHomeworkEntity(modelBuilder);

            ConfigureStudentCourseEntity(modelBuilder);
        }

        private void ConfigureStudentCourseEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder
                .Entity<StudentCourse>(entity =>
                {
                    entity.HasOne(c => c.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(s => s.StudentId);

                    entity.HasOne(s => s.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(c => c.CourseId);
                });
        }

        private void ConfigureHomeworkEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Homework>()
                .HasKey(h => h.HomeworkId);

            modelBuilder
                .Entity<Homework>()
                .HasOne(h => h.Course)
                .WithMany(c => c.HomeworkSubmissions);

            modelBuilder
                .Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeworkSubmissions);

            modelBuilder
                .Entity<Homework>()
                .Property(c => c.Content)
                .IsUnicode(false);
        }

        private void ConfigureResourceEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Resource>()
                .HasKey(r => r.ResourceId);

            modelBuilder
                .Entity<Resource>(entity =>
                {
                    entity
                    .HasOne(r => r.Course)
                    .WithMany(c => c.Resources);
                });

            modelBuilder
                .Entity<Resource>()
                .Property(n => n.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Resource>()
                .Property(u => u.Url)
                .IsUnicode(false);   
        }

        private void ConfigureCourseEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder
                .Entity<Course>()
                .Property(n => n.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Course>()
                .Property(d => d.Description)
                .IsUnicode()
                .IsRequired(false);
        }

        private void ConfigureStudentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder
                .Entity<Student>()
                .Property(n => n.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder
                .Entity<Student>()
                .Property(pn => pn.PhoneNumber)
                .HasColumnType("char(10)");
        }
    }
}
