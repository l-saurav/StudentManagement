using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record UpdateStudentCommand(int StudentID, StudentEntity student): IRequest<StudentReadDTO>;
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentReadDTO>
    {
        private readonly IStudentRepository _studentRepository;
        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentReadDTO> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(request.StudentID);
            if(existingStudent is null)
            {
                return null;
            }
            var updatedStudent = await _studentRepository.UpdateStudentAsync(request.StudentID, request.student);
            return new StudentReadDTO
            {
                StudentID = updatedStudent.StudentID,
                FullName = updatedStudent.FullName,
                DateOfBirth = updatedStudent.DateOfBirth,
                Email = updatedStudent.Email,
                PhoneNumber = updatedStudent.PhoneNumber,
                RegistrationDate = updatedStudent.RegistrationDate
            };
        }
    }
}
