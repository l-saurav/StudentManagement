using StudentManagement.Domain.Entities;
using MediatR;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Application.DTOs;
using AutoMapper;

namespace StudentManagement.Application.Commands
{
    public record UpdateEnrollmentCommand(int EnrollmentID, EnrollmentEntity enrollment) : IRequest<EnrollmentReadDTO>;

    public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, EnrollmentReadDTO>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public UpdateEnrollmentCommandHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<EnrollmentReadDTO> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var existingEnrollment = await _enrollmentRepository.GetByIdAsync(request.EnrollmentID);
            if (existingEnrollment is null)
            {
                return null;
            }
            var enrollmentToUpdate = await _enrollmentRepository.UpdateAsync(request.EnrollmentID, request.enrollment);
            return _mapper.Map<EnrollmentReadDTO>(enrollmentToUpdate);
        }
    }
}
