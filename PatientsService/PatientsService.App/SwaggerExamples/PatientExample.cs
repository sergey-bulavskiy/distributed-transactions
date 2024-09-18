using NSwag.Examples;
using PatientsService.Features.Patients.Dto;

namespace PatientsService.SwaggerExamples;

public class PatientExample : IExampleProvider<CreatePatientDto>
{
    private List<string> _firstNames = ["Garvel", "Ezekiel", "Tarik", "Horus"];
    private List<string> _lastNames = ["Loken", "Abaddon", "Torgadon", "Aximand"];
    public CreatePatientDto GetExample()
    {
        var nameRnd = Random.Shared.Next(0, 3);
        var year = Random.Shared.Next(1950, 2000);

        return new CreatePatientDto()
        {
            DateOfBirth = DateOnly.FromDateTime(new DateTime(year, 1, 1)),
            FirstName = _firstNames[nameRnd],
            LastName = _lastNames[nameRnd]
        };
    }
}