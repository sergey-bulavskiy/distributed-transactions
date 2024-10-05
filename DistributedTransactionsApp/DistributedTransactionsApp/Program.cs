using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres", port: 5432);
var medicalDb = postgres.AddDatabase("medicalDb");

var patientsDataDb = postgres.AddDatabase("patientsDb");


var medicalDataService = builder.AddProject<MedicalDataService_App>("medical-data")
    .WithReference(medicalDb);
/*

var patientsDataService = builder.AddProject<PatientsService_App>("patients-data")
    .WithExternalHttpEndpoints()
    .WithReference(patientsDataDb, optional: false)
    .WithReference(medicalDataService);

builder
    .AddProject<Frontend_App>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(patientsDataService);
*/

builder.Build().Run();