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
    public record GetGradeByIdQuery(int GradeID) : IRequest<GradeEntity>;
    public class GetGradeByIdQueryHandler(IGradeRepository gradeRepository): IRequestHandler<GetGradeByIdQuery,GradeEntity>
    {
        public async Task<GradeEntity> Handle(GetGradeByIdQuery request, CancellationToken cancellationToken)
        {
            return await gradeRepository.GetGradeByIdAsync(request.GradeID);
        }
    }
}
