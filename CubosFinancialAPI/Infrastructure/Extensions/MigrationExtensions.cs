using Microsoft.EntityFrameworkCore;

namespace CubosFinancialAPI.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {

            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ConnectionContext connectionContext =
                scope.ServiceProvider.GetRequiredService<ConnectionContext>();

            connectionContext.Database.Migrate();
        }
    }
}
