using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedicalDataService.Persistence;

/// <summary>
/// This class is to allow running powershell EF commands from the project folder without
/// specifying Startup class (without triggering the whole startup during EF operations
/// like add/remove migrations).
/// </summary>
public class PostgresDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MedicalDataContext>
{
    public MedicalDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MedicalDataContext>();

        optionsBuilder
            .UseNpgsql(
                "Server=localhost;Database=medical_records_db;Port=5432;Username=postgres;Password=postgres;Pooling=true;Keepalive=5;Command Timeout=60;"
            );

        return new MedicalDataContext(optionsBuilder.Options);
    }
}