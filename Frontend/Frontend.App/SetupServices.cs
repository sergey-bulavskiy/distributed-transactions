using Frontend.Http;

namespace Frontend.App;

public static class SetupServices
{
    public static void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        services.AddHttpClient<PatientsServiceClient>(client =>
        {
            string? host = configuration.GetSection("PatientsServiceClientSettings").GetValue<string>("Host");
            client.BaseAddress = new Uri(host);
        });
    }
}