using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetAllCoursesQuery: IRequest<IEnumerable<CourseEntity>>;
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseEntity>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<IEnumerable<CourseEntity>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _courseRepository.GetAllAsync();
        }
    }
}
