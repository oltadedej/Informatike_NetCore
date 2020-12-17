using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityDb_Infor.Domain;

namespace UniversityDb_Infor.DAL
{
  public  class UniversityContext:DbContext
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Student> Student { get; set; }

        //heq pluralizimin e emrave te tabeles
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
        public UniversityContext(DbContextOptions<UniversityContext> options)
             : base(options)
        {
            Database.EnsureCreated();

        }
    }
}
