using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record UpdateCourseCommand(int CourseID, CourseEntity course) : IRequest<CourseReadDTO>;

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseReadDTO>
    {
        private readonly ICourseRepository _courseRepository;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<CourseReadDTO> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(request.CourseID);
            if(existingCourse is null)
            {
                return null;
            }
            var courseToUpdate = await _courseRepository.UpdateCourseAsync(request.CourseID, request.course);
            return new CourseReadDTO
            {
                CourseID = courseToUpdate.CourseID,
                CourseName = courseToUpdate.CourseName,
                CourseCode = courseToUpdate.CourseCode,
                CreditHours = courseToUpdate.CreditHours
            };
        }
    }
}
