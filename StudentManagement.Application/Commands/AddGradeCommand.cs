using AutoMapper;
using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddGradeCommand(GradeEntity gradeEntity) : IRequest<GradeReadDTO>;

    public class AddGradeCommandHandler: IRequestHandler<AddGradeCommand, GradeReadDTO>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;
        public AddGradeCommandHandler(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }
        public async Task<GradeReadDTO> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            var gradeToAdd = await _gradeRepository.AddAsync(request.gradeEntity);
            return _mapper.Map<GradeReadDTO>(gradeToAdd);
        }
    }
}
