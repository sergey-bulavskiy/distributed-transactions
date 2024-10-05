using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("RabbitMQConnection");

var postgres = builder.AddPostgres("postgres", port: 5432)
    .WithDataVolume()
    .WithPgAdmin();
var medicalDb = postgres.AddDatabase("medicalDb");

var patientsDataDb = postgres.AddDatabase("patientsDb");

var medicalDataService = builder.AddProject<MedicalDataService_App>("medical-data")
    .WithReference(messaging)
    .WithReference(medicalDb);

var patientsDataService = builder.AddProject<PatientsService_App>("patients-data")
    .WithExternalHttpEndpoints()
    .WithReference(patientsDataDb, optional: false)
    .WithReference(messaging)
    .WithReference(medicalDataService);

builder
    .AddProject<Frontend_App>("frontend")
    .WithExternalHttpEndpoints()
    .WithReference(patientsDataService);


builder.Build().Run();