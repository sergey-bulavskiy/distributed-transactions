using MedicalDataService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalDataService.Setup;

public class SetupDatabase
{
    public static void AddDatabase(WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // this is needed for OpenIdDict and must go before .UseOpenIddict()
        services.AddMemoryCache(options =>
        {
            options.SizeLimit = null;
        });

        services
            .AddEntityFrameworkNpgsql()
            .AddDbContext<MedicalDataContext>(
                (provider, opt) =>
                {
                    opt.UseNpgsql(connectionString);
                }
            );

        services
            .AddScoped<Func<MedicalDataContext>>(provider => () => CreateDbContext(provider));
    }
    
    public static MedicalDataContext CreateDbContext(IServiceProvider provider)
    {
        return new MedicalDataContext(provider.GetRequiredService<DbContextOptions<MedicalDataContext>>());
    }

    public static async Task RunMigration(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        await using var context = serviceProvider.GetRequiredService<MedicalDataContext>();
        await context.Database.MigrateAsync();
    }
}