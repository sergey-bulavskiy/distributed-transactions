using Libs;
using Libs.Messages;

namespace MedicalDataService.Consumers;

public class DeletePatientMedicalRecordConsumer(MedicalRecordsService medicalRecordsService, ILogger<PatientCreatedConsumer> logger)
    : MessageConsumerBase<DeletePatientMedicalRecord>(logger)
{
    private readonly MedicalRecordsService _medicalRecordsService = medicalRecordsService;
    protected override async Task ConsumeMessage(DeletePatientMedicalRecord message)
    {
        await _medicalRecordsService.DeleteMedicalRecord(message.PatientId);
    }
}