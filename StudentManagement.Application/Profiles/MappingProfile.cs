using AutoMapper;

namespace StudentManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Domain.Entities.StudentEntity, DTOs.StudentReadDTO>();
            CreateMap<DTOs.StudentCreateDTO, Domain.Entities.StudentEntity>()
                .ForMember(dest => dest.StudentID, opt => opt.Ignore());
            CreateMap<DTOs.StudentUpdateDTO, Domain.Entities.StudentEntity>()
                .ForMember(dest => dest.StudentID, opt => opt.Ignore());

            CreateMap<Domain.Entities.CourseEntity, DTOs.CourseReadDTO>();
            CreateMap<DTOs.CourseCreateDTO, Domain.Entities.CourseEntity>()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());
            CreateMap<DTOs.CourseUpdateDTO, Domain.Entities.CourseEntity>()
                .ForMember(dest => dest.CourseID, opt => opt.Ignore());

            CreateMap<Domain.Entities.EnrollmentEntity, DTOs.EnrollmentReadDTO>();
            CreateMap<DTOs.EnrollmentCreateDTO, Domain.Entities.EnrollmentEntity>()
                .ForMember(dest => dest.EnrollmentID, opt => opt.Ignore());
            CreateMap<DTOs.EnrollmentUpdateDTO, Domain.Entities.EnrollmentEntity>()
                .ForMember(dest => dest.EnrollmentID, opt => opt.Ignore());

            CreateMap<Domain.Entities.GradeEntity, DTOs.GradeReadDTO>();
            CreateMap<DTOs.GradeCreateDTO, Domain.Entities.GradeEntity>()
                .ForMember(dest => dest.GradeID, opt => opt.Ignore());
            CreateMap<DTOs.GradeUpdateDTO, Domain.Entities.GradeEntity>()
                .ForMember(dest => dest.GradeID, opt => opt.Ignore());
        }
    }
}
