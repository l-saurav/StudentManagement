using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetEnrollmentByIdQuery(int EnrollmentID) : IRequest<EnrollmentEntity>;

    public class GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepository) : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentEntity>
    {
        public async Task<EnrollmentEntity> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentID);
        }
    }
}
