using Libs;
using MedicalDataService;
using MedicalDataService.Setup;
using NSwag.Examples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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


    app.UseSwagger();
    app.UseSwaggerUI();


await SetupDatabase.RunMigration(app);
