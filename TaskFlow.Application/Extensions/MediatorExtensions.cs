using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TaskFlow.Application.Extensions;

public static class MediatorExtensions
{
    /// <summary>
    /// Registra os serviços do MediatR para a camada de Application.
    /// </summary>
    /// <param name="services">O contêiner de serviços.</param>
    /// <returns>O contêiner de serviços atualizado.</returns>
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        // Registrar todos os handlers definidos na camada Application
        services.AddMediatR(typeof(MediatorExtensions).Assembly);

        return services;
    }
}

