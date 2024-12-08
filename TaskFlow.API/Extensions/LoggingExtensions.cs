using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TaskFlow.API.Extensions;

public static class LoggingExtensions
{
    /// <summary>
    /// Configura o Serilog como provedor de log para a aplicação.
    /// </summary>
    /// <param name="builder">Host builder da aplicação.</param>
    /// <returns>Host builder atualizado.</returns>
    public static IHostBuilder UseSerilogLogging(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration) // Lê a configuração do appsettings.json
                .Enrich.FromLogContext()
                .WriteTo.Console() // Logs no console
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day); // Logs em arquivo
        });

        return builder;
    }
}

