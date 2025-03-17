using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteGradeCommand(int GradeID): IRequest<bool>;
    public class  DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand, bool>
    {
        private readonly IGradeRepository _gradeRepository;
        public DeleteGradeCommandHandler(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public async Task<bool> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            return await _gradeRepository.DeleteAsync(request.GradeID);
        }
    }
}
