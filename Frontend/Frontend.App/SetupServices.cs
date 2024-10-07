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

        string? host = configuration.GetSection("Services")
                .GetSection("patients-data")
                .GetValue<string>("http");

        services.AddHttpClient<PatientsServiceClient>(client =>
        {
            client.BaseAddress = new Uri(host);
        });
    }
}