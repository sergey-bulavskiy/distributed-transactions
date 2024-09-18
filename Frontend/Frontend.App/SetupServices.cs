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
            client.BaseAddress = new Uri("http://localhost:5000");
        });
    }
}