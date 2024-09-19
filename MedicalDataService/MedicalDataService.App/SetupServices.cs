namespace MedicalDataService;

public static class SetupServices
{
    public static void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        services.AddScoped<MedicalRecordsService>();
    }
}