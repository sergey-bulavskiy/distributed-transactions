using Frontend.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Frontend.App.Components.Pages;

public partial class Patients()
{
    [Inject]
    private PatientsServiceClient _patientsServiceClient { get; set; }
    private List<PatientDto>? _patients;

    protected override async Task OnInitializedAsync()
    {
        _patients = await _patientsServiceClient.GetPatients();
    }
}