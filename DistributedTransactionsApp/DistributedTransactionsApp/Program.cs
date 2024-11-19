using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithImage("masstransit/rabbitmq");

var postgres = builder.AddPostgres("postgres", port: 5432)
    .WithDataVolume()
    .WithPgAdmin();

var medicalDb = postgres.AddDatabase("medicalDb");

var patientsDataDb = postgres.AddDatabase("patientsDb");

builder.AddProject<MedicalDataService_App>("medical-data")
    .WaitFor(messaging)
    .WaitFor(medicalDb)
    .WithReference(messaging, "RabbitMQConnection")
    .WithReference(medicalDb, "DefaultConnection");

var patientsDataService = builder.AddProject<PatientsService_App>("patients-data")
    .WithExternalHttpEndpoints()
    .WaitFor(messaging)
    .WaitFor(patientsDataDb)
    .WithReference(patientsDataDb, "DefaultConnection")
    .WithReference(messaging, "RabbitMQConnection");

builder
    .AddProject<Frontend_App>("frontend")
    .WithExternalHttpEndpoints()
    .WaitFor(patientsDataService)
    .WithReference(patientsDataService);


builder.Build().Run();