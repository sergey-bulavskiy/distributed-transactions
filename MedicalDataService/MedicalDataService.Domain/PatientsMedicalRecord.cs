namespace MedicalDataService.Domain;

public class PatientsMedicalRecord(Guid id, Guid patientsId)
{
    public Guid Id { get; init; } = id;

    public Guid PatientsId { get; init; } = patientsId;
}