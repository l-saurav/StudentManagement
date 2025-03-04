using StudentManagement.Domain.Entities;
using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Commands
{
    public record AddStudentCommand(StudentEntity Student) : IRequest<StudentReadDTO>;

    public class AddStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<AddStudentCommand, StudentReadDTO>
    {
        public async Task<StudentReadDTO> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToAdd = await studentRepository.AddStudentAsync(request.Student);
            return new StudentReadDTO
            {
                StudentID = studentToAdd.StudentID,
                FullName = studentToAdd.FullName,
                DateOfBirth = studentToAdd.DateOfBirth,
                Email = studentToAdd.Email,
                PhoneNumber = studentToAdd.PhoneNumber,
                RegistrationDate = studentToAdd.RegistrationDate
            };
        }
    }
}
