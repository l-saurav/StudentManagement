
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteCourseCommand(int CourseId) : IRequest<bool>;
    public class DeleteCourseCommandHandler(ICourseRepository courseRepository) : IRequestHandler<DeleteCourseCommand, bool>
    {
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await courseRepository.DeleteCourseAsync(request.CourseId);
        }
    }
}
