using MediatR;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Queries
{
    public record GetCourseByIdQuery(int CourseID): IRequest<CourseEntity>;
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseEntity>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CourseEntity> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _courseRepository.GetCourseByIdAsync(request.CourseID);
        }
    }
}
