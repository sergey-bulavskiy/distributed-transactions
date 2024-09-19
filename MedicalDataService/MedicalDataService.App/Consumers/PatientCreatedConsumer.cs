using Libs;
using Libs.Messages;

namespace MedicalDataService.Consumers;

public class PatientCreatedConsumer(MedicalRecordsService medicalRecordsService, ILogger<PatientCreatedConsumer> logger)
    : MessageConsumerBase<CreatePatientMedicalRecord>(logger)
{
    private readonly MedicalRecordsService _medicalRecordsService = medicalRecordsService;

    protected override async Task ConsumeMessage(CreatePatientMedicalRecord message)
    {
        await _medicalRecordsService.CreateMedicalRecord(message.Id);
    }
}