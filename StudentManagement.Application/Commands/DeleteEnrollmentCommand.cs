using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteEnrollmentCommand(int EnrollmentID) : IRequest<bool>;
    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, bool>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<bool> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await _enrollmentRepository.DeleteAsync(request.EnrollmentID);
        }
    }
}
