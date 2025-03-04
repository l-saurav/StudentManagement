using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Commands
{
    public record AddEnrollmentCommand(EnrollmentEntity enrollmentEntity) : IRequest<EnrollmentReadDTO>;

    public class AddEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository) : IRequestHandler<AddEnrollmentCommand, EnrollmentReadDTO>
    {
        public async Task<EnrollmentReadDTO> Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollmentToAdd = await enrollmentRepository.AddEnrollmentAsync(request.enrollmentEntity);
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
