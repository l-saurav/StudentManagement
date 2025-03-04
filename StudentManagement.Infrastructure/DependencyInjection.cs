using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace StudentManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StudentManagementDBContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        return services;
    }
}