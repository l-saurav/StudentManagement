using AutoMapper;
using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record UpdateGradeCommand(int GradeID, GradeEntity Grade) : IRequest<GradeReadDTO>;
    public class UpdateGradeCommandHandler: IRequestHandler<UpdateGradeCommand, GradeReadDTO>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;
        public UpdateGradeCommandHandler(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }
        public async Task<GradeReadDTO> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var existingGrade = await _gradeRepository.GetGradeByIdAsync(request.GradeID);
            if (existingGrade is null)
            {
                return null;
            }
            var gradeToUpdate = await _gradeRepository.UpdateGradeAsync(request.GradeID, request.Grade);
            return _mapper.Map<GradeReadDTO>(gradeToUpdate);
        }
    }
}
