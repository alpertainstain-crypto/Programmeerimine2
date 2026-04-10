using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KooliProjekt.Application.Data;

public class Doctor : Entity
{

    [Required(ErrorMessage = "Doktori nimi on kohustuslik")]
    [MaxLength(225, ErrorMessage = "Nimi ei saa olla pikem kui 225 märki")]
    [MinLength(1, ErrorMessage = "Nimi peab sisaldama vähemalt 1 märki")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Spetsialiseerumine on kohustuslik")]
    [MaxLength(225, ErrorMessage = "Spetsialiseerumine ei saa olla pikem kui 225 märki")]
    [MinLength(1, ErrorMessage = "Spetsialiseerumine peab sisaldama vähemalt 1 märki")]
    public string Specialty { get; set; } = default!;

    public List<Appointment> Appointments { get; set; } = new();
}