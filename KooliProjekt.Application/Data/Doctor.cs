using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "Eesnimi on kohustuslik")]
    public string FirstName { get; set; } = default!;
    [Required(ErrorMessage = "Perekonnanimi on kohustuslik")]
    public string LastName { get; set; } = default!;
    [Required(ErrorMessage = "E-mail on kohustuslik")]

    public string Specialty { get; set; } = default!;
    [Required(ErrorMessage = "Eriala on kohustuslik")]

    public List<Availability> Availabilities { get; set; } = new List<Availability>();
}