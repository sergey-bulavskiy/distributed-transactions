namespace PatientsService.Domain;

public class Patient(string firstName, string lastName, DateOnly dateOfBirth)
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Id of the medical data record in separate micro-service.
    /// Null unless data is created.
    /// </summary>
    public Guid? MedicalDataId { get; set; }

    /// <summary>
    /// First name of the patient. 
    /// </summary>
    public string FirstName { get; set; } = firstName;

    /// <summary>
    /// Last name of the patient.
    /// </summary>
    public string LastName { get; set; } = lastName;

    /// <summary>
    /// Date of birth of the patient.
    /// </summary>
    public DateOnly DateOfBirth { get; set; } = dateOfBirth;
}