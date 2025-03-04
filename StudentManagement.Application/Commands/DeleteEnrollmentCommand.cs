using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteEnrollmentCommand(int EnrollmentID) : IRequest<bool>;
    public class DeleteEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository) : IRequestHandler<DeleteEnrollmentCommand, bool>
    {
        public async Task<bool> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await enrollmentRepository.DeleteEnrollmentAsync(request.EnrollmentID);
        }
    }
}
