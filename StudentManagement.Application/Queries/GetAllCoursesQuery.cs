using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetAllCoursesQuery: IRequest<IEnumerable<CourseEntity>>;
    public class GetAllCoursesQueryHandler(ICourseRepository courseRepository) : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseEntity>>
    {
        public async Task<IEnumerable<CourseEntity>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await courseRepository.GetCourses();
        }
    }
}
