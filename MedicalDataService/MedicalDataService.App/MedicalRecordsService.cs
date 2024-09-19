using Libs.Messages;
using MassTransit;
using MedicalDataService.Domain;
using MedicalDataService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalDataService;

public class MedicalRecordsService(
    MedicalDataContext medicalDataContext,
    ILogger<MedicalRecordsService> logger,
    IPublishEndpoint publishEndpoint)
{
    private readonly MedicalDataContext _medicalDataContext = medicalDataContext;
    private readonly ILogger<MedicalRecordsService> _logger = logger;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task CreateMedicalRecord(Guid patientsId)
    {
        var medicalRecordId = Guid.NewGuid();

        var patientsMedicalRecord = new PatientsMedicalRecord(medicalRecordId, patientsId);
        _medicalDataContext.MedicalRecords.Add(patientsMedicalRecord);

        await _medicalDataContext.SaveChangesAsync();

        _logger.LogInformation($"Medical record for patient {patientsId} has been created.");

        await _publishEndpoint.Publish(
            new MedicalRecordCreated(PatientId: patientsId, MedicalRecordId: medicalRecordId));
    }

    public async Task DeleteMedicalRecord(Guid patientId)
    {
        _logger.LogInformation("Deleting medical record for {id}", patientId);

        await _medicalDataContext.MedicalRecords.Where(x => x.PatientsId == patientId)
            .ExecuteDeleteAsync();
    }
}