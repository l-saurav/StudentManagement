using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteGradeCommand(int GradeID): IRequest<bool>;
    public class  DeleteGradeCommandHandler(IGradeRepository gradeRepository): IRequestHandler<DeleteGradeCommand, bool>
    {
        public async Task<bool> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            return await gradeRepository.DeleteGradeAsync(request.GradeID);
        }
    }
}
