using Libs.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PatientsService.Domain;
using PatientsService.Http;
using PatientsService.Persistence;

namespace PatientsService.Features.Patients;

public class PatientsAppService(
    PatientsContext dbContext,
    ILogger<PatientsAppService> logger,
    IPublishEndpoint publishEndpoint)
{
    private PatientsContext _dbContext = dbContext;
    private IPublishEndpoint _publishEndpoint = publishEndpoint;
    private ILogger<PatientsAppService> _logger = logger;

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

        await _publishEndpoint.Publish(new CreatePatientMedicalRecord() { Id = patient.Id });

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

    public async Task UpdateMedicalRecord(Guid patientId, Guid medicalRecordId)
    {
        //TODO: Add transactions.

        Patient? patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == patientId);

        if (patient == null)
            throw new Exception("Patient not found.");

        patient.MedicalDataId = medicalRecordId;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePatient(Guid patientId)
    {
        _logger.LogInformation("Deleting patient: {id}", patientId);

        await _dbContext.Patients.Where(p => p.Id == patientId).ExecuteDeleteAsync();
        await _publishEndpoint.Publish(new DeletePatientMedicalRecord(patientId));
    }
}