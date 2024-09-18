using System.Net.Http.Json;

namespace Frontend.Http;

public class PatientsServiceClient
{
    private readonly HttpClient _httpClient;

    public PatientsServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<PatientDto>?> GetPatients()
    {
        return await _httpClient.GetFromJsonAsync<List<PatientDto>>("/patients");
    } 
}

public class PatientDto(Guid id, string firstName, string lastName, DateOnly dateOfBirth)
{
    public Guid Id { get; private set; } = id;

    /// <summary>
    /// First name of the patient. 
    /// </summary>
    public string FirstName { get; set; } = firstName;

    /// <summary>
    /// Last name of the patient.
    /// </summary>
    public string LastName { get; set; } = lastName;

    /// <summary>
    /// Date of birth of the patient.
    /// </summary>
    public DateOnly DateOfBirth { get; set; } = dateOfBirth;
}