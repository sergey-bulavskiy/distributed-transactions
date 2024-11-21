using Microsoft.AspNetCore.Mvc;

namespace MedicalDataService.Features;

[Route("[controller]")]
public class MedicalDataController(MedicalRecordsService medicalRecordsService) : ControllerBase
{
    private MedicalRecordsService _medicalRecordsService = medicalRecordsService;
    
    [HttpGet]
    public async Task<List<PatientsMedicalRecordDto>> GetPatients()
    {
        return await _medicalRecordsService.GetMedicalRecords();
    }
}