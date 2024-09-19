namespace PatientsService.Http;

public record PatientDto(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth);
