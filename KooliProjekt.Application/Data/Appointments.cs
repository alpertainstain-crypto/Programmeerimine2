using System;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vastuvõtu aeg on kohustuslik")]
    public DateTime Time { get; set; }

    [Required(ErrorMessage = "Kasutaja ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Kasutaja ID peab olema suurem kui 0")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Kasutaja on kohustuslik")]
    public User User { get; set; } = default!;

    [Required(ErrorMessage = "Doktori ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Doktori ID peab olema suurem kui 0")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Doktor on kohustuslik")]
    public Doctor Doctor { get; set; } = default!;
}
