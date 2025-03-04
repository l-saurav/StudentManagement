using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Queries
{
    public record GetStudentByIdQuery(int StudentID) : IRequest<StudentEntity>;
    public class GetStudentByIdQueryHandler(IStudentRepository studentRepository) : IRequestHandler<GetStudentByIdQuery, StudentEntity>
    {
        public async Task<StudentEntity> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetStudentByIdAsync(request.StudentID);
        }
    }
}
