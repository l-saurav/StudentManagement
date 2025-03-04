using StudentManagement.Domain.Entities;
using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.DTOs;

namespace StudentManagement.Application.Commands
{
    public record AddStudentCommand(StudentEntity Student) : IRequest<StudentReadDTO>;

    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, StudentReadDTO>
    {
        private readonly IStudentRepository _studentRepository;
        public AddStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentReadDTO> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToAdd = await _studentRepository.AddStudentAsync(request.Student);
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
