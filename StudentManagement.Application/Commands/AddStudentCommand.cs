using StudentManagement.Domain.Entities;
using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.DTOs;
using AutoMapper;

namespace StudentManagement.Application.Commands
{
    public record AddStudentCommand(StudentEntity Student) : IRequest<StudentReadDTO>;

    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, StudentReadDTO>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public AddStudentCommandHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<StudentReadDTO> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToAdd = await _studentRepository.AddStudentAsync(request.Student);
            return _mapper.Map<StudentReadDTO>(studentToAdd);
        }
    }
}
