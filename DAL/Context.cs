using EFCoreTutorials.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTutorials.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<StudentAdresses> StudentsAdresses { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Grades> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One-To-Many Relationship
            modelBuilder.Entity<Students>()
                .HasOne<Grades>(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId)
                .OnDelete(DeleteBehavior.Cascade);

            //One-To-One Relationship
            modelBuilder.Entity<Students>()
                .HasOne<StudentAdresses>(s => s.StudentAdress)
                .WithOne(ad => ad.Student)
                .HasForeignKey<StudentAdresses>(ad => ad.AddressOfStudentId);

            //Many-To-Many Relationship

            //Creating composite key
            modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            //Making the relationships
            modelBuilder.Entity<StudentCourses>()
                .HasOne<Students>(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourses>()
                .HasOne<Courses>(sc => sc.Course)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
