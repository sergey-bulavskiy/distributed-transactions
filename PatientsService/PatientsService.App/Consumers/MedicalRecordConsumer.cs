using Libs;
using Libs.Messages;
using MassTransit;
using PatientsService.Features.Patients;

namespace PatientsService.Consumers;

public class MedicalRecordConsumer(PatientsAppService patientsAppService, ILogger<MedicalRecordConsumer> logger) : MessageConsumerBase<MedicalRecordCreated>(logger)
{
    private readonly PatientsAppService _patientsAppService = patientsAppService;

    protected override async Task ConsumeMessage(MedicalRecordCreated message)
    {
        await _patientsAppService.UpdateMedicalRecord(message.PatientId, message.MedicalRecordId);
    }
}