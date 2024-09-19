using Microsoft.AspNetCore.Mvc;
using PatientsService.Domain;
using PatientsService.Http;

namespace PatientsService.Features.Patients;

[Route("[controller]")]
public class PatientsController(PatientsAppService patientsAppService) : ControllerBase
{
    private readonly PatientsAppService _patientsAppService = patientsAppService;


    [HttpPost]
    public async Task CreatePatient([FromBody] CreatePatientDto createPatientDto)
    {
        await _patientsAppService.CreatePatient(createPatientDto);
    }

    [HttpGet]
    public async Task<List<PatientDto>> GetPatients()
    {
        return await _patientsAppService.GetPatients();
    }

    [HttpDelete]
    [Route("{patientId:guid}")]
    public async Task DeletePatient(Guid patientId)
    {
        await _patientsAppService.DeletePatient(patientId);
    }
    
}