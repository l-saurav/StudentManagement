
using MediatR;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record DeleteCourseCommand(int CourseId) : IRequest<bool>;
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await _courseRepository.DeleteCourseAsync(request.CourseId);
        }
    }
}
