using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddGradeCommand(GradeEntity gradeEntity) : IRequest<GradeReadDTO>;

    public class AddGradeCommandHandler(IGradeRepository gradeRepository): IRequestHandler<AddGradeCommand, GradeReadDTO>
    {
        public async Task<GradeReadDTO> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            var gradeToAdd = await gradeRepository.AddGradeAsync(request.gradeEntity);
            return new GradeReadDTO
            {
                GradeID = gradeToAdd.GradeID,
                StudentID = gradeToAdd.StudentID,
                CourseID = gradeToAdd.CourseID,
                Grade = gradeToAdd.Grade
            };
        }
    }
}
