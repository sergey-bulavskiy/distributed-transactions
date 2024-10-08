﻿using Libs;

namespace PatientsService;

public static class SetupServices
{
    public static void AddServices(
        IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        services
            .AddScoped<Features.Patients.PatientsAppService>();
    }
}