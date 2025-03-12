using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalController : ControllerBase
    {
        private readonly StudentManagementDBContext _context;

        public AdditionalController(StudentManagementDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("EnrolledStudent")]
        public async Task<IActionResult> GetAllStudentWithCourses()
        {
            var studentCourseQuery = from e in _context.Enrollments
                                      join s in _context.Students on e.StudentID equals s.StudentID
                                      join c in _context.Courses on e.CourseID equals c.CourseID
                                      select new
                                      {
                                          s.StudentID,
                                          s.FullName,
                                          c.CourseID,
                                          c.CourseName
                                      };
            return Ok(await studentCourseQuery.ToListAsync());
        }

        [HttpGet]
        [Route("UnenrolledStudent")]
        public async Task<IActionResult> GetAllUnenrolledStudent()
        {
            var unenrolledStudentQuery = from s in _context.Students
                                         join e in _context.Enrollments on s.StudentID equals e.StudentID
                                         into lt
                                         from rt in lt.DefaultIfEmpty()
                                         where rt == null
                                         select new
                                         {
                                             s.StudentID,
                                             s.FullName
                                         };

            return Ok(await unenrolledStudentQuery.ToListAsync());
        }

        [HttpGet]
        [Route("UnenrolledCourse")]
        public async Task<IActionResult> GetAllUnenrolledCourse()
        {
            var unenrolledCourseQuery = from c in _context.Courses
                                        join e in _context.Enrollments on c.CourseID equals e.CourseID
                                        into lt
                                        from rt in lt.DefaultIfEmpty()
                                        where rt == null
                                        select new
                                        {
                                            c.CourseID,
                                            c.CourseName
                                        };
            return Ok(await unenrolledCourseQuery.ToListAsync());
        }

        [HttpGet]
        [Route("FailedStudent")]
        public async Task<IActionResult> GetAllFailedStudent()
        {
            var failedStudentQuery = (from g in _context.Grades
                                     join s in _context.Students
                                     on g.StudentID equals s.StudentID
                                     where g.Grade == 'F'
                                     select new
                                     {
                                         s.StudentID,
                                         s.FullName
                                     }).Distinct();
            return Ok(await failedStudentQuery.ToListAsync());
        }

        [HttpGet("test-sync")]
        public async Task<IActionResult> TestSync()
        {
      
            Console.WriteLine("Inside controller");
            return Ok("Synchronization Complete");
        }
    }
}
