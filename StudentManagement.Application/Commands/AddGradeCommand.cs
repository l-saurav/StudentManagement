using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddGradeCommand(GradeEntity gradeEntity) : IRequest<GradeReadDTO>;

    public class AddGradeCommandHandler: IRequestHandler<AddGradeCommand, GradeReadDTO>
    {
        private readonly IGradeRepository _gradeRepository;
        public AddGradeCommandHandler(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public async Task<GradeReadDTO> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            var gradeToAdd = await _gradeRepository.AddGradeAsync(request.gradeEntity);
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
