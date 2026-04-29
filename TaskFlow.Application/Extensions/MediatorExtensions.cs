using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.Common.Behaviors;

namespace TaskFlow.Application.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(MediatorExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}

