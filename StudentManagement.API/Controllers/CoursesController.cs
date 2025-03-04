using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Commands;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Queries;
using StudentManagement.Domain.Entities;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ISender sender, IMapper mapper,IValidator<AddCourseCommand> addValidator, IValidator<UpdateCourseCommand> updValidator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddCourseAsync([FromBody] CourseCreateDTO courseDTO)
        {
            var courseEntity = mapper.Map<CourseEntity>(courseDTO);
            // **Validate before hitting database**
            var validationResult = await addValidator.ValidateAsync(new AddCourseCommand(courseEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new AddCourseCommand(courseEntity));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var result = await sender.Send(new GetAllCoursesQuery());
            var courseDTO = mapper.Map<IEnumerable<CourseReadDTO>>(result);
            return Ok(courseDTO);
        }

        [HttpGet("{courseID}")]
        public async Task<IActionResult> GetCourseByIdAsync([FromRoute] int courseID)
        {
            var result = await sender.Send(new GetCourseByIdQuery(courseID));
            if (result is null)
            {
                return NotFound();
            }
            var courseDTO = mapper.Map<CourseReadDTO>(result);
            return Ok(courseDTO);
        }

        [HttpPut("{courseID}")]
        public async Task<IActionResult> UpdateCourseAsync([FromRoute] int courseID, [FromBody] CourseUpdateDTO courseDTO)
        {
            var courseEntity = mapper.Map<CourseEntity>(courseDTO);
            courseEntity.CourseID = courseID;
            // **Validate before hitting database**
            var validationResult = await updValidator.ValidateAsync(new UpdateCourseCommand(courseID,courseEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new UpdateCourseCommand(courseID, courseEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{courseID}")]
        public async Task<IActionResult> DeleteCourseAsync([FromRoute] int courseID)
        {
            var result = await sender.Send(new DeleteCourseCommand(courseID));
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
