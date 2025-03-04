using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalController(StudentManagementDBContext context) : ControllerBase
    {
        [HttpGet("students-with-courses")]
        public async Task<IActionResult> GetStudentsWithCourses()
        {
            var studentCourses = await context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new
                {
                    e.Student.StudentID,
                    Student_Name = e.Student.FullName,
                    e.Course.CourseID,
                    Enrolled_Course = e.Course.CourseName
                })
                .ToListAsync();
            return Ok(studentCourses);

        }

        [HttpGet("students-without-enrollment")]
        public async Task<IActionResult> GetStudentsWithoutEnrollments()
        {
            var unenrolledStudents = await context.Students
                .GroupJoin(
                    context.Enrollments,
                    student => student.StudentID,
                    enrollment => enrollment.StudentID,
                    (student, enrollments) => new { student, enrollments }
                )
                .Where(x => !x.enrollments.Any()) // Filters students who have no enrollments
                .Select(x => new
                {
                    x.student.StudentID,
                    Unenrolled_Student = x.student.FullName
                })
                .ToListAsync();
            return Ok(unenrolledStudents);
        }

        [HttpGet("courses-without-enrollment")]
        public async Task<IActionResult> GetCoursesWithoutEnrollments()
        {
            var unenrolledCourses = await context.Courses
                .GroupJoin(
                    context.Enrollments,
                    course => course.CourseID,
                    enrollment => enrollment.CourseID,
                    (course, enrollments) => new { course, enrollments }
                )
                .Where(x => !x.enrollments.Any()) // Filters courses that have no enrollments
                .Select(x => new
                {
                    x.course.CourseID,
                    Unenrolled_Course = x.course.CourseName
                })
                .ToListAsync();
            return Ok(unenrolledCourses);
        }

        [HttpGet("students-with-failed-courses")]
        public async Task<IActionResult> GetStudentsWithFailedCourses()
        {
            // Fetching students who have at least one 'F' grade
            var failedStudents = await context.Students
                .Join(
                    context.Grades,
                    student => student.StudentID,
                    grade => grade.StudentID,
                    (student, grade) => new { student, grade }
                )
                .Where(x => x.grade.Grade == 'F')  // Filter for students who have failed at least one course
                .Select(x => new
                {
                    x.student.StudentID,
                    x.student.FullName
                })
                .Distinct() // Ensure unique students
                .ToListAsync();

            return Ok(failedStudents);
        }

        [HttpGet("calculate-gpa/{studentId}")]
        public async Task<IActionResult> CalculateStudentGPA(int studentId)
        {
            // Get student name
            var student = await context.Students
                .Where(s => s.StudentID == studentId)
                .Select(s => new { s.FullName })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            // Get grades and calculate total credits and grade points
            var gradeData = await context.Grades
                .Where(g => g.StudentID == studentId)
                .Join(
                    context.Courses,
                    g => g.CourseID,
                    c => c.CourseID,
                    (g, c) => new
                    {
                        g.Grade,
                        c.CreditHours
                    })
                .ToListAsync();

            if (gradeData.Count == 0)
            {
                return BadRequest(new { message = "No grades available for this student." });
            }

            // Calculate total credits, total grade points, failed courses, and enrolled courses
            int totalCredits = gradeData.Sum(g => g.CreditHours);
            decimal totalGradePoints = gradeData.Sum(g =>
                g.Grade == 'A' ? 4 * g.CreditHours :
                g.Grade == 'B' ? 3 * g.CreditHours :
                g.Grade == 'C' ? 2 * g.CreditHours :
                g.Grade == 'D' ? 1 * g.CreditHours : 0);
            int failedCourses = gradeData.Count(g => g.Grade == 'F');
            int enrolledCourses = gradeData.Count();

            // Check if student is enrolled in at least 5 courses
            if (enrolledCourses < 5)
            {
                return Ok(new { student.FullName, GPA = "Not Enough Courses" });
            }

            // Check if student has failed any course
            if (failedCourses > 0)
            {
                return Ok(new { student.FullName, GPA = "Failed" });
            }

            // Calculate GPA
            decimal gpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;

            return Ok(new
            {
                student.FullName,
                GPA = gpa.ToString("F2") // Format GPA to 2 decimal places
            });
        }


    }
}
