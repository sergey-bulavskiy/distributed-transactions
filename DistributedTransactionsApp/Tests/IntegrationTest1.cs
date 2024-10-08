using System.Net.Http.Json;
using MedicalDataService;
using MedicalDataService.Features;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using PatientsService.Http;

namespace Tests.Tests;

public class IntegrationTest1
{
    // Instructions:
    // 1. Add a project reference to the target AppHost project, e.g.:
    //
    //    <ItemGroup>
    //        <ProjectReference Include="../MyAspireApp.AppHost/MyAspireApp.AppHost.csproj" />
    //    </ItemGroup>
    //
    // 2. Uncomment the following example test and update 'Projects.MyAspireApp_AppHost' to match your AppHost project:
    //
    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.DistributedTransactionsApp>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging

        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var patientsDataClient = app.CreateHttpClient("patients-data");
        var medicalDataClient = app.CreateHttpClient("medical-data");

        await resourceNotificationService.WaitForResourceAsync("patients-data", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
        await resourceNotificationService.WaitForResourceAsync("medical-data", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
        await resourceNotificationService.WaitForResourceAsync("rabbitmq", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));

        // Magic wait.
        //await Task.Delay(1000 * 15);

        var guid = Guid.NewGuid();
        var patientToCreate = new CreatePatientDto("firstName_" + guid.ToString("N").Substring(0, 5), "lastName",
            DateOnly.FromDateTime(DateTime.Now));

        HttpResponseMessage responseMessage = await patientsDataClient.PostAsJsonAsync("/Patients", patientToCreate);
        Assert.True(responseMessage.IsSuccessStatusCode);

        List<PatientDto>? patientsDataRecords =
            await patientsDataClient.GetFromJsonAsync<List<PatientDto>>("/patients");

        Assert.NotNull(patientsDataRecords);

        List<PatientsMedicalRecordDto>? medicalRecords =
            await medicalDataClient.GetFromJsonAsync<List<PatientsMedicalRecordDto>>("/MedicalData");

        Assert.NotNull(medicalRecords);

        var patientRecord = Assert.Single(patientsDataRecords, r => r.FirstName == patientToCreate.FirstName);
        Assert.Single(medicalRecords, r => r.PatientsId == patientRecord.Id);
    }
}