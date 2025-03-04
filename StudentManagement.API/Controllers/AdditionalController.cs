using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("Get")]
        public async Task<IActionResult> GetAllStudentWithCourses()
        {
            var studentQuery = from s in _context.Students
                               select s.FullName;
            return Ok(await studentQuery.ToListAsync());
        }
    }
}
