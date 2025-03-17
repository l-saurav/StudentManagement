using AutoMapper;
using MediatR;
using StudentManagement.Application.DTOs;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application.Commands
{
    public record AddEnrollmentCommand(EnrollmentEntity enrollmentEntity) : IRequest<EnrollmentReadDTO>;

    public class AddEnrollmentCommandHandler : IRequestHandler<AddEnrollmentCommand, EnrollmentReadDTO>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public AddEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<EnrollmentReadDTO> Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollmentToAdd = await _enrollmentRepository.AddAsync(request.enrollmentEntity);
            return _mapper.Map<EnrollmentReadDTO>(enrollmentToAdd);
        }
    }
}
