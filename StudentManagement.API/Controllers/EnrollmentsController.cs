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
    public class EnrollmentsController(ISender sender, IMapper mapper,IValidator<AddEnrollmentCommand> addValidator,IValidator<UpdateEnrollmentCommand> updValidator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddEnrollmentAsync([FromBody] EnrollmentCreateDTO enrollmentDTO)
        {
            var enrollmentEntity = mapper.Map<EnrollmentEntity>(enrollmentDTO);
            // **Validate before hitting database**
            var validationResult = await addValidator.ValidateAsync(new AddEnrollmentCommand(enrollmentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new AddEnrollmentCommand(enrollmentEntity));
            return Ok(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            var result = await sender.Send(new GetAllEnrollmentsQuery());
            var enrollmentDTO = mapper.Map<IEnumerable<EnrollmentReadDTO>>(result);
            return Ok(enrollmentDTO);
        }
        [HttpGet("{enrollmentID}")]
        public async Task<IActionResult> GetEnrollmentByIdAsync([FromRoute] int enrollmentID)
        {
            var result = await sender.Send(new GetEnrollmentByIdQuery(enrollmentID));
            if (result is null)
            {
                return NotFound();
            }
            var enrollmentDTO = mapper.Map<EnrollmentReadDTO>(result);
            return Ok(enrollmentDTO);
        }
        [HttpPut("{enrollmentID}")]
        public async Task<IActionResult> UpdateEnrollmentAsync([FromRoute] int enrollmentID, [FromBody] EnrollmentUpdateDTO enrollmentDTO)
        {
            var enrollmentEntity = mapper.Map<EnrollmentEntity>(enrollmentDTO);
            enrollmentEntity.EnrollmentID = enrollmentID;
            // **Validate before hitting database**
            var validationResult = await updValidator.ValidateAsync(new UpdateEnrollmentCommand(enrollmentID,enrollmentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new UpdateEnrollmentCommand(enrollmentID, enrollmentEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("{enrollmentID}")]
        public async Task<IActionResult> DeleteEnrollmentAsync([FromRoute] int enrollmentID)
        {
            var result = await sender.Send(new DeleteEnrollmentCommand(enrollmentID));
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
