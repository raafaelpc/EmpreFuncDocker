using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmpresaFuncionario.Persistence.StartupService
{
    public class EntityFrameworkCoreMigrator
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ApplyMigrationsWithRetryAsync()
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<EntityFrameworkCoreMigrator>>();

            const int maxRetryCount = 10;
            int retryCount = 0;
            bool dbReady = false;

            while (retryCount < maxRetryCount && !dbReady)
            {
                try
                {
                    logger.LogInformation("Applying database migrations...");
                    using var serviceScope = _serviceProvider.CreateScope();
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated();
                    dbReady = true;
                    logger.LogInformation("Database migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    retryCount++;
                    logger.LogError(ex, $"Database migration failed. Retry {retryCount}/{maxRetryCount}");

                    if (retryCount >= maxRetryCount)
                    {
                        logger.LogError("Max retry count reached. Unable to apply database migrations.");
                        throw;
                    }

                    await Task.Delay(2000);
                }
            }
        }

        //private Task Migrate()
        //{
        //    using var serviceScope = _serviceProvider.CreateScope();
        //    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    context.Database.EnsureCreatedAsync();
       // }
    }
}
