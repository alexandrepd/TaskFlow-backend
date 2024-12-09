using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Services;
using TaskFlow.Infrastructure.Repositories;
using TaskFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TaskFlow.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra os serviços da camada de Infrastructure no contêiner IoC.
    /// </summary>
    /// <param name="services">O contêiner de serviços.</param>
    /// <returns>O contêiner de serviços atualizado.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Adicionar o DbContext ao pipeline de serviços
        services.AddDbContext<TaskDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


        // Registrar o serviço JWT
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        // Outros serviços de Infrastructure podem ser registrados aqui
        // services.AddScoped<IOtherService, OtherService>();

        return services;
    }
}

