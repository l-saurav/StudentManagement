using AutoMapper;
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
        private readonly IMapper _mapper;
        public AddCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CourseReadDTO> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var courseToRead = await _courseRepository.AddAsync(request.Course);
            return _mapper.Map<CourseReadDTO>(courseToRead);
        }
    }
}
