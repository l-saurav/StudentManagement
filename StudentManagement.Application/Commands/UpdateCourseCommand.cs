using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CourseReadDTO> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(request.CourseID);
            if(existingCourse is null)
            {
                return null;
            }
            var courseToUpdate = await _courseRepository.UpdateAsync(request.CourseID, request.course);
            return _mapper.Map<CourseReadDTO>(courseToUpdate);
        }
    }
}
