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
    public class GradesController(ISender sender, IMapper mapper,IValidator<AddGradeCommand> addValidator, IValidator<UpdateGradeCommand> updValidator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddGradeAsync([FromBody] GradeCreateDTO gradeDTO)
        {
            var gradeEntity = mapper.Map<GradeEntity>(gradeDTO);
            // **Validate before hitting database**
            var validationResult = await addValidator.ValidateAsync(new AddGradeCommand(gradeEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new AddGradeCommand(gradeEntity));
            return Ok(result);
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllGradesAsync()
        {
            var result = await sender.Send(new GetAllGradesQuery());
            var gradeDTO = mapper.Map<IEnumerable<GradeReadDTO>>(result);
            return Ok(gradeDTO);
        }
        [HttpGet("{gradeID}")]
        public async Task<IActionResult> GetGradeByIdAsync([FromRoute] int gradeID)
        {
            var result = await sender.Send(new GetGradeByIdQuery(gradeID));
            if (result is null)
            {
                return NotFound();
            }
            var gradeDTO = mapper.Map<GradeReadDTO>(result);
            return Ok(gradeDTO);
        }
        [HttpPut("{gradeID}")]
        public async Task<IActionResult> UpdateGradeAsync([FromRoute] int gradeID, [FromBody] GradeUpdateDTO gradeDTO)
        {
            var gradeEntity = mapper.Map<GradeEntity>(gradeDTO);
            gradeEntity.GradeID = gradeID;
            // **Validate before hitting database**
            var validationResult = await updValidator.ValidateAsync(new UpdateGradeCommand(gradeID, gradeEntity));
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage });

                return BadRequest(new { Errors = errors });
            }
            // **End of validation**
            var result = await sender.Send(new UpdateGradeCommand(gradeID, gradeEntity));
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("{gradeID}")]
        public async Task<IActionResult> DeleteGradeAsync([FromRoute] int gradeID)
        {
            var result = await sender.Send(new DeleteGradeCommand(gradeID));
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
