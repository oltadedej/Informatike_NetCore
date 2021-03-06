﻿using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDb_Infor.DAL;
using UniversityDb_Infor.Domain;
using UniversityDb_Infor.Domain.Search;
using UniversityDb_Infor.Models;
using UniversityDb_Infor.Services.Contract;
using UniversityDb_Infor.Utils;
using Utf8Json;

namespace UniversityDb_Infor.Services
{
    public class ServiceUniversityDB : BaseService<ServiceUniversityDB>, IServiceUniversityDB
    {
        private readonly string ServiceName = nameof(ServiceUniversityDB);
        private readonly string ModelName = "University";
        private readonly UniversityContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceUniversityDB> _logger;
        public ServiceUniversityDB(UniversityContext phoneBookContext, IMapper mapper, ILogger<ServiceUniversityDB> logger) : base(phoneBookContext, mapper, logger)
        {
            this._dbContext = phoneBookContext ?? throw new ArgumentNullException(nameof(phoneBookContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<EnEntityExistsStatus> StudentExistsAsync(int StudentId)
        {
            _logger.LogInformation($"Checking if {typeof(Student).Name} with id: {StudentId} exists");
            if (StudentId < 1)
            {
                _logger.LogError($"Validation error while checking if {typeof(Student).Name} exists. {typeof(Student).Name} with student id: {StudentId} is not valid");
                return EnEntityExistsStatus.BadRequest;
            }
            return await _dbContext.Student.AsNoTracking().AnyAsync(c => c.StudentId == StudentId) ? EnEntityExistsStatus.Found : EnEntityExistsStatus.NotFound;
        }
        public async Task<StudentModel> GetStudentAsync(int id)
        {
            _logger.LogInformation($"Searching for {typeof(Student).Name} with ID: {id}");
            if (id < 1)
            {
                _logger.LogError($"Validation error while searching for {typeof(Student).Name}. {typeof(Student)} with id: {id} is not valid");
                return null;
            }
            Student student = await _dbContext.Student.AsNoTracking().FirstOrDefaultAsync(i => i.StudentId == id);
            if (student != null)
            {
                return _mapper.Map<StudentModel>(student);
            }
            else
            {
                _logger.LogWarning($"No result found. Searching for {typeof(Student).Name} with id: {id}");
            }
            return null;
        }

        public async Task<StudentModel> CreateStudentAsync(StudentModel item)
        {
            _logger.LogInformation($"Creating new {typeof(Student).Name}");
            Student student = _mapper.Map<Student>(item);
            await AddAsync(student);
            if (await this.SaveChangesAsync())
            {
                return _mapper.Map<StudentModel>(student);
            }
            else
            {
                _logger.LogError($"Save new {typeof(Student).Name} error. {typeof(Student).Name} parameters: {JsonSerializer.ToJsonString(item)}");
            }
            return null;

        }

        public async Task<StudentModel> UpdateStudentAsync(int studentId, StudentModel item)
        {
            _logger.LogInformation($"Updating {typeof(Student).Name} with id: {studentId}");

            Student student = await _dbContext.Student.AsNoTracking().FirstOrDefaultAsync(c => c.StudentId == studentId);

            if (student == null)
            {
                _logger.LogWarning($"Update {typeof(Student).Name} failed. {typeof(Student).Name} with id: {studentId} not found");
                return null;
            }
            _mapper.Map(item, student);
            _dbContext.Entry(student).State = EntityState.Modified;
            if (await this.SaveChangesAsync())
            {
                return _mapper.Map<StudentModel>(student);
            }
            else
            {
                _logger.LogError($"Update {typeof(Student).Name} error. {typeof(Student).Name} with id: {studentId} and parameters: {JsonSerializer.ToJsonString(item)}");
            }
            return null;

        }


        public async Task<IEnumerable<StudentModel>> SearchStudentsAsync(StudentSearchCriteria searchCriteria)
        {

            IQueryable<Student> query = _dbContext.Student.AsNoTracking();
            query = searchCriteria.ApplySearchCriteriaFilter(query);
            List<Student> students = await query.ToListAsync();
            if (!students.IsNullOrEmpty())
            {
                return _mapper.Map<IEnumerable<StudentModel>>(students);
            }
            else
            {
                _logger.LogWarning($"No result found. Searching for {typeof(Student).Name} Type with search parameters: {JsonSerializer.ToJsonString(searchCriteria)}");
            }
            return null;
        }



        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            _logger.LogInformation($"Deleting {typeof(Student).Name}");
            Student student = await _dbContext.Student.AsNoTracking().FirstOrDefaultAsync(i => i.StudentId == studentId);
            if (student == null)
            {
                _logger.LogWarning($"Delete {typeof(Student).Name} failed. {typeof(Student).Name} with Id: {studentId}");
                return false;
            }
            Delete(student);
            if (await this.SaveChangesAsync())
            {
                return true;
            }
            else
            {
                _logger.LogError($"Delete {typeof(Student).Name} error. {typeof(Student).Name} with Id: {studentId}");
            }
            return false;
        }




        public async Task<CourseModel> MaximumCourse(DateTime dt1, DateTime dt2)
        {

            //join three different tables using linq
            var result = from course in _dbContext.Course
                         join enrollment in _dbContext.Enrollment on course.CourseId equals enrollment.CourseId
                         select new { course.CourseId, enrollment.StudentId, course.CourseTitle } into intermediate
                         join student in _dbContext.Student on intermediate.StudentId equals student.StudentId
                         where student.EnrollmentDate >= dt1 && student.EnrollmentDate <= dt2
                         group intermediate by intermediate.CourseId into g
                         select new { courseId = g.Key, Nr_Enrollments_For_Course = g.Count() };
            //select the course id that has the maximum enrollment for the defined period
            var id_course = result.ToList().OrderByDescending(x => x.Nr_Enrollments_For_Course).Select(i => i.courseId).FirstOrDefault();
            var finalresults = _dbContext.Course.Where(i => i.CourseId == id_course).FirstOrDefault();
            return _mapper.Map<CourseModel>(finalresults);

        }

        public async Task<StudentModel> MaximumStudentCredits()
        {

            //join  tables using linq
            var result = from course in _dbContext.Course
                         join enrollment in _dbContext.Enrollment on course.CourseId equals enrollment.CourseId
                         select new { course.CourseId, enrollment.StudentId, course.CourseTitle, enrollment.Grade, course.Credits } into intermediate
                         join student in _dbContext.Student on intermediate.StudentId equals student.StudentId
                         where intermediate.Grade > 4
                         group intermediate by intermediate.StudentId into g
                         select new { studentId = g.Key, MaximumStudentCredits = g.Sum(i => i.Credits) };

            var id_student = result.ToList().OrderByDescending(x => x.MaximumStudentCredits).Select(i => i.studentId).FirstOrDefault();
            var finalresults = _dbContext.Student.Where(i => i.StudentId == id_student).FirstOrDefault();
            return _mapper.Map<StudentModel>(finalresults);

        }

      
        public async Task<IEnumerable<CourseModel>> CurseTeKaluaraPerStudent(int std)
        {
            IQueryable<Course> query = from course in _dbContext.Course
                                       join enrollment in _dbContext.Enrollment on course.CourseId equals enrollment.CourseId
                                       where enrollment.Grade > 4 && enrollment.StudentId == std
                                       select course;

            List<Course> courses = await query.ToListAsync();
            if (!courses.IsNullOrEmpty())
            {
                return _mapper.Map<IEnumerable<CourseModel>>(courses);
            }
            else
            {
                _logger.LogWarning($"No result found.");
            }
            return null;
        }



        public async Task<CourseModel> MaximumEnrollmentForAllTimes()
        {

            //join two different tables using linq
            var result = from course in _dbContext.Course
                         join enrollment in _dbContext.Enrollment on course.CourseId equals enrollment.CourseId
                         group course by course.CourseId into g
                         select new { courseId = g.Key, Nr_Enrollments_For_Course = g.Count() };
            //select the course id that has the maximum enrollment
            var id_course = result.ToList().OrderByDescending(x => x.Nr_Enrollments_For_Course).Select(i => i.courseId).FirstOrDefault();
            var finalresults = _dbContext.Course.Where(i => i.CourseId == id_course).FirstOrDefault();
            return _mapper.Map<CourseModel>(finalresults);

        }

        public async Task<CourseModel> CallDBUsingCMD()
        {
            // Inital Catalog eshte emri i bazes se te dhenave
            //data source eshte emri i serverit
            //integrated security= SSPI/ TRUE --> logim me windows authentication

            string ConString = "data source=.; database=StudentDB; integrated security=SSPI";
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();
                string query = "Select * from students";
                SqlCommand cmd = new SqlCommand("query_to_execute", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);
                var course = Dt.AsEnumerable().Select(row => new Course
                {
                    CourseId = Convert.ToInt32(row["CourseId"]),
                    CourseTitle = Convert.ToString(row["CourseTitle"]),
                    Credits = Convert.ToInt32(row["Credits"]),

                }).FirstOrDefault();


                connection.Close();
                return _mapper.Map<CourseModel>(course);
            }

        }

       
    }
}
