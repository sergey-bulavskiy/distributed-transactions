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
        _dbContext.Patients.Add(new Patient(createPatientDto.FirstName, createPatientDto.LastName,
            createPatientDto.DateOfBirth));

        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Successfully finished patient's creation.");
    }

    public async Task<List<Patient>> GetPatients()
    {
        _logger.LogInformation("Returning list of patients.");

        List<Patient> list = await _dbContext.Patients.ToListAsync();

        return list;
    }
}