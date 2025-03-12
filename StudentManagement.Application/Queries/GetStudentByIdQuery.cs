using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetStudentByIdQuery(int StudentID) : IRequest<StudentEntity>;
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentEntity>
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<StudentEntity> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetByIdAsync(request.StudentID);
        }
    }
}
