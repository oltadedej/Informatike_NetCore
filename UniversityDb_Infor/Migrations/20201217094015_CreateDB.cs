using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDb_Infor.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    IdCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.IdCourse);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IdStudenti = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mbiemer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.IdStudenti);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    IdEnrollment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStudent = table.Column<int>(type: "int", nullable: false),
                    IdCourse = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    StudentIdStudenti = table.Column<int>(type: "int", nullable: true),
                    CourseIdCourse = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.IdEnrollment);
                    table.ForeignKey(
                        name: "FK_Enrollment_Course_CourseIdCourse",
                        column: x => x.CourseIdCourse,
                        principalTable: "Course",
                        principalColumn: "IdCourse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student_StudentIdStudenti",
                        column: x => x.StudentIdStudenti,
                        principalTable: "Student",
                        principalColumn: "IdStudenti",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseIdCourse",
                table: "Enrollment",
                column: "CourseIdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_StudentIdStudenti",
                table: "Enrollment",
                column: "StudentIdStudenti");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
