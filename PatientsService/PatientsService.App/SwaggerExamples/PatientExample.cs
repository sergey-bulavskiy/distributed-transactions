using NSwag.Examples;
using PatientsService.Http;

namespace PatientsService.SwaggerExamples;

public class PatientExample : IExampleProvider<CreatePatientDto>
{
    private List<string> _firstNames = ["Garvel", "Ezekiel", "Tarik", "Horus"];
    private List<string> _lastNames = ["Loken", "Abaddon", "Torgadon", "Aximand"];

    public CreatePatientDto GetExample()
    {
        var nameRnd = Random.Shared.Next(0, 3);
        var year = Random.Shared.Next(1950, 2000);

        return new CreatePatientDto(FirstName: _firstNames[nameRnd], LastName: _lastNames[nameRnd],
            DateOfBirth: DateOnly.FromDateTime(new DateTime(year, 1, 1)));
    }
}