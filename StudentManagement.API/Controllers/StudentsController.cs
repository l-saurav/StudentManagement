using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Domain.Entities;
using StudentManagement.Application.Commands;
using StudentManagement.Application.Queries;
using AutoMapper;
using StudentManagement.Application.DTOs;
using FluentValidation;


namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(ISender sender,IMapper mapper, IValidator<AddStudentCommand> addValidator,IValidator<UpdateStudentCommand> updValidator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCreateDTO studentDTO)
        {
            var studentEntity = mapper.Map<StudentEntity>(studentDTO);

            // **Validate before hitting database**
            var validationResult = await addValidator.ValidateAsync(new AddStudentCommand(studentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**

            var result = await sender.Send(new AddStudentCommand(studentEntity));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var result = await sender.Send(new GetAllStudentsQuery());
            var studentDTO = mapper.Map<IEnumerable<StudentReadDTO>>(result);
            return Ok(studentDTO);
        }

        [HttpGet("{studentID}")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int studentID)
        {
            var result = await sender.Send(new GetStudentByIdQuery(studentID));
            if(result is null)
            {
                return NotFound();
            }
            var studentDTO = mapper.Map<StudentReadDTO>(result);
            return Ok(studentDTO);
        }

        [HttpPut("{studentID}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] int studentID, [FromBody] StudentUpdateDTO studentDTO)
        {
            var studentEntity = mapper.Map<StudentEntity>(studentDTO);
            studentEntity.StudentID = studentID;
            // **Validate before hitting database**
            var validationResult = await updValidator.ValidateAsync(new UpdateStudentCommand(studentID,studentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new UpdateStudentCommand(studentID, studentEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{studentID}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int studentID)
        {
            var result = await sender.Send(new DeleteStudentCommand(studentID));
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
