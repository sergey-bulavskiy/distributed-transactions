using Microsoft.EntityFrameworkCore;
using PatientsService.Domain;
using PatientsService.Features.Patients.Dto;
using PatientsService.Persistence;

namespace PatientsService.Features.Patients;

public class PatientsService(PatientsContext dbContext, ILogger<PatientsService> logger)
{
    private PatientsContext _dbContext = dbContext;
    private ILogger<PatientsService> _logger = logger;

    /// <summary>
    /// Creates patient record in the system,
    /// also sends a notification to other services that patient has been created.
    /// </summary>
    public async Task CreatePatient(CreatePatientDto createPatientDto)
    {
        _logger.LogInformation("Create patient has started.");

        //TODO: Wrap it up in the transaction.
        var patient = new Patient(createPatientDto.FirstName, createPatientDto.LastName, createPatientDto.DateOfBirth);
        _dbContext.Patients.Add(patient);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Successfully finished patient's creation.");
    }

    public async Task<List<PatientDto>> GetPatients()
    {
        _logger.LogInformation("Returning list of patients.");

        List<Patient> list = await _dbContext.Patients.ToListAsync();

        return list
            .Select(p => new PatientDto(p.Id, p.FirstName, p.LastName, p.DateOfBirth))
            .ToList();
    }
}

public class PatientDto(Guid id, string firstName, string lastName, DateOnly dateOfBirth)
{
    public Guid Id { get; private set; } = id;

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