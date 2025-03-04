using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteStudentCommand(int StudentId):IRequest<bool>;
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;
        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            return await _studentRepository.DeleteStudentAsync(request.StudentId);
        }
    }
}
