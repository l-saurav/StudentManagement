﻿
using Microsoft.Extensions.DependencyInjection;

namespace StudentManagement.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services;
    }
}