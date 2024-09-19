using Libs;
using NSwag.Examples;
using PatientsService;
using PatientsService.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
SetupDatabase.AddDatabase(builder);

builder.Services.AddExampleProviders(typeof(Program).Assembly);
builder.Services.AddOpenApiDocument((settings, provider) =>
{
    settings.AddExamples(provider);
});

SetupAspNet.AddAspNet(builder);

SetupMassTransit.AddMassTransit(builder.Services, builder.Configuration);

// Set up your application-specific services here
SetupServices.AddServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

await SetupDatabase.RunMigration(app);
SetupAspNet.UseFrontlineServices(app);
SetupAspNet.UseEndpoints(app);

app.UseOpenApi(options => { options.Path = "/swagger/{documentName}/swagger.json"; });
app.UseSwaggerUi(options => { options.Path = "/swagger"; });

app.Run();