using PatientsService;
using PatientsService.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SetupDatabase.AddDatabase(builder);


SetupAspNet.AddAspNet(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

await SetupDatabase.RunMigration(app);
SetupAspNet.UseFrontlineServices(app);
SetupAspNet.UseEndpoints(app);


app.Run();

namespace PatientsService
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}