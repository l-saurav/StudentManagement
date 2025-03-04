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
    public class EnrollmentsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<AddEnrollmentCommand> _addValidator;
        private readonly IValidator<UpdateEnrollmentCommand> _updValidator;
        public EnrollmentsController(ISender sender, IMapper mapper, IValidator<AddEnrollmentCommand> addValidator, IValidator<UpdateEnrollmentCommand> updValidator)
        {
            _sender = sender;
            _mapper = mapper;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddEnrollmentAsync([FromBody] EnrollmentCreateDTO enrollmentDTO)
        {
            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollmentDTO);
            // **Validate before hitting database**
            var validationResult = await _addValidator.ValidateAsync(new AddEnrollmentCommand(enrollmentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new AddEnrollmentCommand(enrollmentEntity));
            return Ok(result);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllEnrollmentsAsync()
        {
            var result = await _sender.Send(new GetAllEnrollmentsQuery());
            var enrollmentDTO = _mapper.Map<IEnumerable<EnrollmentReadDTO>>(result);
            return Ok(enrollmentDTO);
        }
        [HttpGet]
        [Route("{enrollmentID}")]
        public async Task<IActionResult> GetEnrollmentByIdAsync([FromRoute] int enrollmentID)
        {
            var result = await _sender.Send(new GetEnrollmentByIdQuery(enrollmentID));
            if (result is null)
            {
                return NotFound();
            }
            var enrollmentDTO = _mapper.Map<EnrollmentReadDTO>(result);
            return Ok(enrollmentDTO);
        }
        [HttpPut]
        [Route("{enrollmentID}")]
        public async Task<IActionResult> UpdateEnrollmentAsync([FromRoute] int enrollmentID, [FromBody] EnrollmentUpdateDTO enrollmentDTO)
        {
            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollmentDTO);
            enrollmentEntity.EnrollmentID = enrollmentID;
            // **Validate before hitting database**
            var validationResult = await _updValidator.ValidateAsync(new UpdateEnrollmentCommand(enrollmentID,enrollmentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new UpdateEnrollmentCommand(enrollmentID, enrollmentEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("{enrollmentID}")]
        public async Task<IActionResult> DeleteEnrollmentAsync([FromRoute] int enrollmentID)
        {
            var result = await _sender.Send(new DeleteEnrollmentCommand(enrollmentID));
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
