using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        //Register Synchronization Service
        services.AddScoped<ISynchronizationService, SynchronizationServices>();

        return services;
    }
}