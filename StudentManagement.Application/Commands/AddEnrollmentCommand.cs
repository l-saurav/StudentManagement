using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddEnrollmentCommand(EnrollmentEntity enrollmentEntity) : IRequest<EnrollmentReadDTO>;

    public class AddEnrollmentCommandHandler : IRequestHandler<AddEnrollmentCommand, EnrollmentReadDTO>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        public AddEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
        public async Task<EnrollmentReadDTO> Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollmentToAdd = await _enrollmentRepository.AddEnrollmentAsync(request.enrollmentEntity);
            return new EnrollmentReadDTO
            {
                EnrollmentID = enrollmentToAdd.EnrollmentID,
                StudentID = enrollmentToAdd.StudentID,
                CourseID = enrollmentToAdd.CourseID,
                EnrollmentDate = enrollmentToAdd.EnrollmentDate
            };
        }
    }
}
