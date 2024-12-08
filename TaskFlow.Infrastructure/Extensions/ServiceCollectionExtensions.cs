using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Services;

namespace TaskFlow.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra os serviços da camada de Infrastructure no contêiner IoC.
    /// </summary>
    /// <param name="services">O contêiner de serviços.</param>
    /// <returns>O contêiner de serviços atualizado.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Registrar o serviço JWT
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Outros serviços de Infrastructure podem ser registrados aqui
        // services.AddScoped<IOtherService, OtherService>();

        return services;
    }
}

