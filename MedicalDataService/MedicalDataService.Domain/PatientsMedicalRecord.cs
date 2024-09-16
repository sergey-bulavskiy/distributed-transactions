namespace MedicalDataService.Domain;

public class PatientsMedicalRecord
{
    public Guid Id { get; set; }
    
    public Guid PatientsId { get; set; }
}