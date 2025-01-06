using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TaskFlow.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                // Use either EnsureCreated or Migrate, based on your needs
                dbContext.Database.Migrate();
            }
        }
    }
}