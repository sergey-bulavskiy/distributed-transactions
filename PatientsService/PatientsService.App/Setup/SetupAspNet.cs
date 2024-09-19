using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace PatientsService.Setup;

public static class SetupAspNet
{
    public static void AddAspNet(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        ConfigureSerialization(builder);

        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddControllers(
            (options) =>
            {
                AddGlobalFilters(options);
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            }
        );

        services.AddRazorPages();
        if (builder.Environment.IsDevelopment())
        {
            services.AddRazorPages();
        }

        services.AddHealthChecks();

        builder.Services.AddCors(
            x =>
                x.AddPolicy(
                    DefaultCorsPolicyName,
                    configurePolicy =>
                        configurePolicy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()
                            .WithExposedHeaders("x-miniprofiler-ids")
                )
        );
    }

    private static void ConfigureSerialization(WebApplicationBuilder builder)
    {
        // add DateOnly/TimeOnly support
        // also add UtcEverywhere approach
        builder.Services.AddMvc(options =>
        {
            options.ModelBinderProviders.Insert(0, new UtcDateTimeModelBinderProvider());
        });
        builder.Services
            .AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.SetupJson();
            });
    }

    private const string DefaultCorsPolicyName = "DefaultCorsPolicy";
    private static void AddGlobalFilters(MvcOptions options)
    {
        // This is needed for NSwag to produce correct client code
        options.Filters.Add(
            new ProducesResponseTypeAttribute(typeof(ValidationProblemDetails), 400)
        );
        options.Filters.Add(new ProducesResponseTypeAttribute(200));
    }

    public static void UseFrontlineServices(WebApplication app)
    {
        if (!app.Configuration.GetValue<bool>("DisableCompression"))
            app.UseResponseCompression();

        UseForwardedHeaders(app);
        app.UseRouting();
        app.UseCors(DefaultCorsPolicyName);
    }

    private static void UseForwardedHeaders(IApplicationBuilder app)
    {
        var forwardedHeadersOptions = new ForwardedHeadersOptions
        {
            ForwardedHeaders =
                ForwardedHeaders.XForwardedFor
                | ForwardedHeaders.XForwardedProto
                | ForwardedHeaders.XForwardedHost
        };
        
        // These three subnets encapsulate the applicable Azure subnets. At the moment, it's not possible to narrow it down further.
        // from https://docs.microsoft.com/en-us/azure/app-service/configure-language-dotnetcore?pivots=platform-linux
        // forwardedHeadersOptions.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:10.0.0.0"), 104));
        // forwardedHeadersOptions.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:192.168.0.0"), 112));
        // forwardedHeadersOptions.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:172.16.0.0"), 108));
        forwardedHeadersOptions.KnownNetworks.Clear();
        forwardedHeadersOptions.KnownProxies.Clear();

        app.UseForwardedHeaders(forwardedHeadersOptions);
    }

    public static void UseEndpoints(WebApplication app)
    {
        app.UseStaticFiles();
        app.MapControllers();
        app.MapHealthChecks("/health");

        app.MapFallbackToFile("index.html");
    }
}
