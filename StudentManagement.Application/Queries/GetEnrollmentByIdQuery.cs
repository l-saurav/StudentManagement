using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetEnrollmentByIdQuery(int EnrollmentID) : IRequest<EnrollmentEntity>;

    public class GetEnrollmentByIdQueryHandler : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentEntity>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<EnrollmentEntity> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _enrollmentRepository.GetByIdAsync(request.EnrollmentID);
        }
    }
}
