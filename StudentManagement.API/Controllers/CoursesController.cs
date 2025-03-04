using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Commands;
using StudentManagement.Application.DTOs;
using StudentManagement.Application.Queries;
using StudentManagement.Domain.Entities;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCourseCommand> _addValidator;
        private readonly IValidator<UpdateCourseCommand> _updValidator;
        public CoursesController(ISender sender, IMapper mapper, IValidator<AddCourseCommand> addValidator, IValidator<UpdateCourseCommand> updValidator)
        {
            _sender = sender;
            _mapper = mapper;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddCourseAsync([FromBody] CourseCreateDTO courseDTO)
        {
            var courseEntity = _mapper.Map<CourseEntity>(courseDTO);
            // **Validate before hitting database**
            var validationResult = await _addValidator.ValidateAsync(new AddCourseCommand(courseEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new AddCourseCommand(courseEntity));
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var result = await _sender.Send(new GetAllCoursesQuery());
            var courseDTO = _mapper.Map<IEnumerable<CourseReadDTO>>(result);
            return Ok(courseDTO);
        }

        [HttpGet]
        [Route("{courseID}")]
        public async Task<IActionResult> GetCourseByIdAsync([FromRoute] int courseID)
        {
            var result = await _sender.Send(new GetCourseByIdQuery(courseID));
            if (result is null)
            {
                return NotFound();
            }
            var courseDTO = _mapper.Map<CourseReadDTO>(result);
            return Ok(courseDTO);
        }

        [HttpPut]
        [Route("{courseID}")]
        public async Task<IActionResult> UpdateCourseAsync([FromRoute] int courseID, [FromBody] CourseUpdateDTO courseDTO)
        {
            var courseEntity = _mapper.Map<CourseEntity>(courseDTO);
            courseEntity.CourseID = courseID;
            // **Validate before hitting database**
            var validationResult = await _updValidator.ValidateAsync(new UpdateCourseCommand(courseID,courseEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new UpdateCourseCommand(courseID, courseEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{courseID}")]
        public async Task<IActionResult> DeleteCourseAsync([FromRoute] int courseID)
        {
            var result = await _sender.Send(new DeleteCourseCommand(courseID));
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
