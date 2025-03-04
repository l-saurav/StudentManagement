using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetAllEnrollmentsQuery : IRequest<IEnumerable<EnrollmentEntity>>;
    public class GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository) : IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<EnrollmentEntity>>
    {
        public async Task<IEnumerable<EnrollmentEntity>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            return await enrollmentRepository.GetEnrollments();
        }
    }
}
