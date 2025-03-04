using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record UpdateStudentCommand(int StudentID, StudentEntity student): IRequest<StudentReadDTO>;
    public class UpdateStudentCommandHandler(IStudentRepository studentRepository) : IRequestHandler<UpdateStudentCommand, StudentReadDTO>
    {
        public async Task<StudentReadDTO> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var existingStudent = await studentRepository.GetStudentByIdAsync(request.StudentID);
            if(existingStudent is null)
            {
                return null;
            }
            var updatedStudent = await studentRepository.UpdateStudentAsync(request.StudentID, request.student);
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
