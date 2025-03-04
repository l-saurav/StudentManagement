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
    public class GradesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IValidator<AddGradeCommand> _addValidator;
        private readonly IValidator<UpdateGradeCommand> _updValidator;
        public GradesController(ISender sender, IMapper mapper, IValidator<AddGradeCommand> addValidator, IValidator<UpdateGradeCommand> updValidator)
        {
            _sender = sender;
            _mapper = mapper;
            _addValidator = addValidator;
            _updValidator = updValidator;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddGradeAsync([FromBody] GradeCreateDTO gradeDTO)
        {
            var gradeEntity = _mapper.Map<GradeEntity>(gradeDTO);
            // **Validate before hitting database**
            var validationResult = await _addValidator.ValidateAsync(new AddGradeCommand(gradeEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new AddGradeCommand(gradeEntity));
            return Ok(result);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllGradesAsync()
        {
            var result = await _sender.Send(new GetAllGradesQuery());
            var gradeDTO = _mapper.Map<IEnumerable<GradeReadDTO>>(result);
            return Ok(gradeDTO);
        }
        [HttpGet]
        [Route("{gradeID}")]
        public async Task<IActionResult> GetGradeByIdAsync([FromRoute] int gradeID)
        {
            var result = await _sender.Send(new GetGradeByIdQuery(gradeID));
            if (result is null)
            {
                return NotFound();
            }
            var gradeDTO = _mapper.Map<GradeReadDTO>(result);
            return Ok(gradeDTO);
        }
        [HttpPut]
        [Route("{gradeID}")]
        public async Task<IActionResult> UpdateGradeAsync([FromRoute] int gradeID, [FromBody] GradeUpdateDTO gradeDTO)
        {
            var gradeEntity = _mapper.Map<GradeEntity>(gradeDTO);
            gradeEntity.GradeID = gradeID;
            // **Validate before hitting database**
            var validationResult = await _updValidator.ValidateAsync(new UpdateGradeCommand(gradeID, gradeEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await _sender.Send(new UpdateGradeCommand(gradeID, gradeEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{gradeID}")]
        public async Task<IActionResult> DeleteGradeAsync([FromRoute] int gradeID)
        {
            var result = await _sender.Send(new DeleteGradeCommand(gradeID));
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
