using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PatientsService.Persistence;

/// <summary>
/// This class is to allow running powershell EF commands from the project folder without
/// specifying Startup class (without triggering the whole startup during EF operations
/// like add/remove migrations).
/// </summary>
public class PostgresDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PatientsContext>
{
    public PatientsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PatientsContext>();

        optionsBuilder
            .UseNpgsql(
                "Server=localhost;Database=patients_db;Port=5432;Username=postgres;Password=postgres;Pooling=true;Keepalive=5;Command Timeout=60;"
            );

        return new PatientsContext(optionsBuilder.Options);
    }
}