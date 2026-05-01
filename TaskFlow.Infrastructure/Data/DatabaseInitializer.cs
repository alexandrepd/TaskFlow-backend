using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TaskDbContext>>();

        try
        {
            dbContext.Database.Migrate();
            SeedAdminUser(dbContext, passwordHasher, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private static void SeedAdminUser(TaskDbContext dbContext, IPasswordHasher passwordHasher, ILogger logger)
    {
        const string adminUsername = "admin";

        if (dbContext.Users.Any(u => u.Username == adminUsername))
            return;

        var adminPassword = Environment.GetEnvironmentVariable("TASKFLOW_ADMIN_PASSWORD")
            ?? throw new InvalidOperationException(
                "TASKFLOW_ADMIN_PASSWORD environment variable is not set. " +
                "Set it before starting the application.");

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Username = adminUsername,
            PasswordHash = passwordHasher.Hash(adminPassword),
            Role = "Admin"
        };

        dbContext.Users.Add(admin);
        dbContext.SaveChanges();

        logger.LogInformation("Admin user seeded successfully.");
    }
}