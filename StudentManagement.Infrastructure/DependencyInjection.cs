using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StudentManagementDBContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddDbContext<StudentManagementDBContextBackup>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BackupConnection")));

        // Register Generic Repository for both DbContexts
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();

        services.AddScoped<ISynchronizationService, SynchronizationServices>();

        services.AddScoped<IStudentRepositoryBackup, StudentRepositoryBackup>();

        return services;
    }
}
