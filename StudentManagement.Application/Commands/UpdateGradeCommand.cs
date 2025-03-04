using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record UpdateGradeCommand(int GradeID, GradeEntity Grade) : IRequest<GradeReadDTO>;
    public class UpdateGradeCommandHandler(IGradeRepository gradeRepository): IRequestHandler<UpdateGradeCommand, GradeReadDTO>
    {
        public async Task<GradeReadDTO> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var existingGrade = await gradeRepository.GetGradeByIdAsync(request.GradeID);
            if (existingGrade is null)
            {
                return null;
            }
            var gradeToUpdate = await gradeRepository.UpdateGradeAsync(request.GradeID, request.Grade);
            return new GradeReadDTO
            {
                GradeID = gradeToUpdate.GradeID,
                StudentID = gradeToUpdate.StudentID,
                CourseID = gradeToUpdate.CourseID,
                Grade = gradeToUpdate.Grade
            };
        }
    }
}
