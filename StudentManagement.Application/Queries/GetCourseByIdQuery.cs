using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetCourseByIdQuery(int CourseID): IRequest<CourseEntity>;
    public class GetCourseByIdQueryHandler(ICourseRepository courseRepository) : IRequestHandler<GetCourseByIdQuery, CourseEntity>
    {
        public async Task<CourseEntity> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await courseRepository.GetCourseByIdAsync(request.CourseID);
        }
    }
}
