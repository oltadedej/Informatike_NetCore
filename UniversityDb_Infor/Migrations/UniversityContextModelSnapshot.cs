﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityDb_Infor.DAL;

namespace UniversityDb_Infor.Migrations
{
    [DbContext(typeof(UniversityContext))]
    partial class UniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("UniversityDb_Infor.Domain.Course", b =>
                {
                    b.Property<int>("IdCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CourseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCourse");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("UniversityDb_Infor.Domain.Enrollment", b =>
                {
                    b.Property<int>("IdEnrollment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CourseIdCourse")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("IdCourse")
                        .HasColumnType("int");

                    b.Property<int>("IdStudent")
                        .HasColumnType("int");

                    b.Property<int?>("StudentIdStudenti")
                        .HasColumnType("int");

                    b.HasKey("IdEnrollment");

                    b.HasIndex("CourseIdCourse");

                    b.HasIndex("StudentIdStudenti");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("UniversityDb_Infor.Domain.Student", b =>
                {
                    b.Property<int>("IdStudenti")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Emer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mbiemer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdStudenti");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("UniversityDb_Infor.Domain.Enrollment", b =>
                {
                    b.HasOne("UniversityDb_Infor.Domain.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseIdCourse");

                    b.HasOne("UniversityDb_Infor.Domain.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentIdStudenti");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityDb_Infor.Domain.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("UniversityDb_Infor.Domain.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
