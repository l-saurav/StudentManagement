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
    public class StudentsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<AddStudentCommand> _addValidator;
        private readonly IValidator<UpdateStudentCommand> _updValidator;
        public StudentsController(ISender sender, IMapper mapper, IValidator<AddStudentCommand> addValidator, IValidator<UpdateStudentCommand> updValidator)
        {
            _sender = sender;
            _mapper = mapper;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentCreateDTO studentDTO)
        {
            var studentEntity = _mapper.Map<StudentEntity>(studentDTO);

            // **Validate before hitting database**
            var validationResult = await _addValidator.ValidateAsync(new AddStudentCommand(studentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**

            var result = await _sender.Send(new AddStudentCommand(studentEntity));
            return Ok(result);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var result = await _sender.Send(new GetAllStudentsQuery());
            var studentDTO = _mapper.Map<IEnumerable<StudentReadDTO>>(result);
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{studentID}")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int studentID)
        {
            var result = await _sender.Send(new GetStudentByIdQuery(studentID));
            if(result is null)
            {
                return NotFound();
            }
            var studentDTO = _mapper.Map<StudentReadDTO>(result);
            return Ok(studentDTO);
        }

        [HttpPut]
        [Route("{studentID}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] int studentID, [FromBody] StudentUpdateDTO studentDTO)
        {
            var studentEntity = _mapper.Map<StudentEntity>(studentDTO);
            studentEntity.StudentID = studentID;
            // **Validate before hitting database**
            var validationResult = await _updValidator.ValidateAsync(new UpdateStudentCommand(studentID,studentEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new UpdateStudentCommand(studentID, studentEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{studentID}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int studentID)
        {
            var result = await _sender.Send(new DeleteStudentCommand(studentID));
            if (result)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
