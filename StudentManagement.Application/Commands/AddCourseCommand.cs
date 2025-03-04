using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddCourseCommand(CourseEntity Course) : IRequest<CourseReadDTO>;
    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, CourseReadDTO>
    {
        private readonly ICourseRepository _courseRepository;
        public AddCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CourseReadDTO> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToRead = await _courseRepository.AddCourseAsync(request.Course);
            return new CourseReadDTO
            {
                CourseID = courseToRead.CourseID,
                CourseName = courseToRead.CourseName,
                CourseCode = courseToRead.CourseCode,
                CreditHours = courseToRead.CreditHours
            };
        }
    }
}
