using Frontend.Http;
using Microsoft.AspNetCore.Components;
using PatientsService.Http;

namespace Frontend.App.Components.Pages;

public partial class Patients()
{
    [Inject] private PatientsServiceClient _patientsServiceClient { get; set; }
    private List<PatientDto>? _patients;

    protected override async Task OnInitializedAsync()
    {
        _patients = await _patientsServiceClient.GetPatients();
    }

    private async Task CreateRandomPatient()
    {
        await _patientsServiceClient.CreatePatient(RandomPatientService.GenerateRandomPatient());
        _patients = await _patientsServiceClient.GetPatients();
    }

    private async Task DeleteTopPatient()
    {
        var patientToDelete = _patients?.FirstOrDefault();

        if (patientToDelete != null)
        {
            await _patientsServiceClient.DeletePatient(patientToDelete.Id);
            _patients = await _patientsServiceClient.GetPatients();
        }
    }
}