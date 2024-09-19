using PatientsService.Http;

namespace Frontend.App;

public static class RandomPatientService
{
    private static readonly List<string> FirstNames =
    [
        "Garvel",
        "Ezekiel",
        "Tarik",
        "Horus",
        "Marcus",
        "Corien",
        "Marius",
        "Donatos",
        "Erasmus",
    ];

    private static readonly List<string> LastNames =
    [
        "Loken",
        "Abaddon",
        "Torgadon",
        "Aximand",
        "Sumatris",
        "Heilbron",
        "Reinhart",
        "Aphael",
        "Tycho",
    ];

    public static CreatePatientDto GenerateRandomPatient()
    {
        var firstNameRnd = Random.Shared.Next(0, FirstNames.Count);
        var lastNameRnd = Random.Shared.Next(0, LastNames.Count);
        var year = Random.Shared.Next(1950, 2000);

        return new CreatePatientDto(FirstName: FirstNames[firstNameRnd], LastName: LastNames[lastNameRnd],
            DateOfBirth: DateOnly.FromDateTime(new DateTime(year, 1, 1)));
    }
}