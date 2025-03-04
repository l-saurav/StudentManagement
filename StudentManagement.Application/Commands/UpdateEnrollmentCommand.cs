using StudentManagement.Domain.Entities;
using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Commands
{
    public record UpdateEnrollmentCommand(int EnrollmentID, EnrollmentEntity enrollment) : IRequest<EnrollmentReadDTO>;

    public class UpdateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository) : IRequestHandler<UpdateEnrollmentCommand, EnrollmentReadDTO>
    {
        public async Task<EnrollmentReadDTO> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var existingEnrollment = await enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentID);
            if (existingEnrollment is null)
            {
                return null;
            }
            var enrollmentToUpdate = await enrollmentRepository.UpdateEnrollmentAsync(request.EnrollmentID, request.enrollment);
            return new EnrollmentReadDTO
            {
                EnrollmentID = enrollmentToUpdate.EnrollmentID,
                StudentID = enrollmentToUpdate.StudentID,
                CourseID = enrollmentToUpdate.CourseID,
                EnrollmentDate = enrollmentToUpdate.EnrollmentDate
            };
        }
    }
}
