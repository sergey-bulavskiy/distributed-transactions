using System.ComponentModel.DataAnnotations;

namespace PatientsService.Features.Patients.Dto;

public class CreatePatientDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateOnly DateOfBirth { get; set; }
}