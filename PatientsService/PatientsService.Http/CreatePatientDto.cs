namespace PatientsService.Http;

public record CreatePatientDto(string FirstName, string LastName, DateOnly DateOfBirth);
