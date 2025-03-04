using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetAllStudentsQuery : IRequest<IEnumerable<StudentEntity>>;
    public class GetAllStudentsQueryHandler(IStudentRepository studentRepository) : IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentEntity>>
    {
        public async Task<IEnumerable<StudentEntity>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetStudents();
        }
    }
}
