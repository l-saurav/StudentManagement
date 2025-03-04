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
    public record GetAllGradesQuery: IRequest<IEnumerable<GradeEntity>>;
    public class GetAllGradesQueryHandler(IGradeRepository gradeRepository): IRequestHandler<GetAllGradesQuery, IEnumerable<GradeEntity>>
    {
        public async Task<IEnumerable<GradeEntity>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
        {
            return await gradeRepository.GetGrades();
        }
    }
}
