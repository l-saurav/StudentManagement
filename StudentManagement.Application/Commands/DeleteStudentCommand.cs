using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteStudentCommand(int StudentId):IRequest<bool>;
    public class DeleteStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<DeleteStudentCommand, bool>
    {
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            return await studentRepository.DeleteStudentAsync(request.StudentId);
        }
    }
}
