namespace PatientsService.Features.Patients;

public record PatientDto(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth);
