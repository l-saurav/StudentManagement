using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetAllEnrollmentsQuery : IRequest<IEnumerable<EnrollmentEntity>>;
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<EnrollmentEntity>>
    {
        public readonly IEnrollmentRepository _enrollmentRepository;
        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<IEnumerable<EnrollmentEntity>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            return await _enrollmentRepository.GetEnrollments();
        }
    }
}
