using Microsoft.AspNetCore.Mvc;
using PatientsService.Domain;
using PatientsService.Features.Patients.Dto;

namespace PatientsService.Features.Patients;

[Route("[controller]")]
public class PatientsController(PatientsService patientsService) : ControllerBase
{
    private readonly PatientsService _patientsService = patientsService;


    [HttpPost]
    public async Task CreatePatient([FromBody] CreatePatientDto createPatientDto)
    {
        await _patientsService.CreatePatient(createPatientDto);
    }

    [HttpGet]
    public async Task<List<PatientDto>> GetPatients()
    {
        return await _patientsService.GetPatients();
    }
    
}