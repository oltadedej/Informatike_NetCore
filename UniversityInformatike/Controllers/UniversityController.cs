﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UniversityDb_Infor.Configuration;
using UniversityDb_Infor.Domain;
using UniversityDb_Infor.Domain.Search;
using UniversityDb_Infor.Models;
using UniversityDb_Infor.Services;
using UniversityDb_Infor.Services.Contract;
using UniversityDb_Infor.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityInformatike.Controllers
{

    [Route("api/University")]
    [ApiController]
    public class UniversityController : ControllerBase
    {

        #region Private Fields


        private readonly IServiceUniversityDB _serviceUniversityDB;
        private readonly IMapper _mapper;
        private readonly CacheConfiguration cacheConfiguration;
        private readonly ILogger<UniversityController> _logger;


        public UniversityController(IServiceUniversityDB phoneBookService,
                                           IMapper mapper,
                                           ILogger<UniversityController> logger
            ,
                                           IOptions<CacheConfiguration> options
            )
        {
            this._serviceUniversityDB = phoneBookService ?? throw new ArgumentNullException(nameof(phoneBookService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cacheConfiguration = options.Value;
        }
        #endregion

        //, [FromServices] StaticCache staticCache

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> Get([FromQuery] StudentSearchCriteria studentSearchCriteria,
            [FromServices] StaticCache staticCache)
        {
            _logger.LogInformation("Test cache methods ");
            ApplicationsAuthorized application = null;
            if (cacheConfiguration.IsDebugMode)
            {
                if (StaticCache.GetApplicationsAuthorized().Count > 0)
                {
                    _logger.LogInformation("Search if application is authorized for this method and exist nel cache");
                    application = StaticCache.GetApplicationsAuthorized().Where(i => i.idApplication == cacheConfiguration.DebugIDApplication).FirstOrDefault();

                }
                else
                { //add result into cache
                    _logger.LogInformation("Application is not authorized from cache, add app into chache list ");
                    application = new ApplicationsAuthorized();
                    application.idApplication = cacheConfiguration.DebugIDApplication;
                    application.isAuthorized = true;
                    staticCache.AddAplicationIntoCache(application);
                }
            }

            //if (application != null)
            //{
            //    if (application.isAuthorized == true)
            //    {

            //    }

            //}


            IEnumerable<StudentModel> students = await _serviceUniversityDB.SearchStudentsAsync(studentSearchCriteria);
            if (students != null && students.Any())
            {
                return Ok(students.ToList());
            }
            return NoContent();
        }



        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<ActionResult<StudentModel>> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Id {id} is not valid");
            }

            StudentModel student = await _serviceUniversityDB.GetStudentAsync(id);
            if (student != null)
            {
                return Ok(student);
            }
            return NotFound($"Id {id} is not valid");
        }



        [HttpPost]
        public async Task<ActionResult<StudentModel>> Post([FromBody] StudentModel Student)
        {
            StudentModel studentReturn = await _serviceUniversityDB.CreateStudentAsync(Student);
            if (studentReturn != null)
            {
                return CreatedAtRoute("GetStudent", new { id = studentReturn.StudentId }, studentReturn);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<StudentModel>> Put(int id, [FromBody] StudentModel student)
        {
            if (await _serviceUniversityDB.StudentExistsAsync(id) != EnEntityExistsStatus.Found)
            {
                return BadRequest($"Id {id} is not valid");
            }

            StudentModel studentModelUpdated = await _serviceUniversityDB.UpdateStudentAsync(id, student);
            if (studentModelUpdated != null)
            {
                return Ok(studentModelUpdated);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _serviceUniversityDB.StudentExistsAsync(id) != EnEntityExistsStatus.Found)
            {
                return BadRequest($"Id {id} is not valid");
            }

            if (await _serviceUniversityDB.DeleteStudentAsync(id))
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }

        /// <summary>
        /// Get couse detail that has the maximum of enrollment beetween two dates
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        [HttpGet("/course", Name = "GetCourseWithMaximumEnrollmentForDefinedDates")]
        public async Task<ActionResult<CourseModel>> GetCourseWithMaximumEnrollmentForDefinedDates(DateTime d1, DateTime d2)
        {
            if (d1 == DateTime.MinValue || d2 == DateTime.MinValue)
            {
                return BadRequest($"Dates are not valid");
            }

            CourseModel course = await _serviceUniversityDB.MaximumCourse(d1, d2);
            if (course != null)
            {
                return Ok(course);
            }
            return NotFound($"Dates are not valid");
        }

        /// <summary>
        /// Get course details that has the maximum enrollments
        /// </summary>
        /// <returns></returns>
        [HttpGet("/course/maximum", Name = "GetCourseWithMaximumEnrollment")]
        public async Task<ActionResult<CourseModel>> GetCourseWithMaximumEnrollment()
        {

            CourseModel course = await _serviceUniversityDB.MaximumEnrollmentForAllTimes();
            if (course != null)
            {
                return Ok(course);
            }
            return NoContent();
        }
    }
}
