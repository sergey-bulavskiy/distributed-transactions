using Microsoft.EntityFrameworkCore;
using PatientsService.Persistence;

namespace PatientsService.Setup;

public class SetupDatabase
{
    public static void AddDatabase(WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;

        var connectionString = configuration.GetConnectionString("patientsDb");

        // this is needed for OpenIdDict and must go before .UseOpenIddict()
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = null;
        });

        services
            .AddDbContextPool<PatientsContext>(
                (provider, opt) =>
                {
                    opt.UseNpgsql(connectionString);
                }
            );

        services
            .AddScoped<Func<PatientsContext>>(provider => () => CreateDbContext(provider));
    }
    
    public static PatientsContext CreateDbContext(IServiceProvider provider)
    {
        return new PatientsContext(provider.GetRequiredService<DbContextOptions<PatientsContext>>());
    }

    public static async Task RunMigration(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        await using var context = serviceProvider.GetRequiredService<PatientsContext>();
        await context.Database.MigrateAsync();
    }
}