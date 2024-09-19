using System.Net.Http.Json;
using PatientsService.Http;

namespace Frontend.Http;

public class PatientsServiceClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<PatientDto>?> GetPatients()
    {
        return await _httpClient.GetFromJsonAsync<List<PatientDto>>("/patients");
    }

    public async Task CreatePatient(CreatePatientDto dto)
    {
        await _httpClient.PostAsJsonAsync("/patients", dto);
    }

    public async Task DeletePatient(Guid patientId)
    {
        
    }
}