using System;
using System.ComponentModel.DataAnnotations;

public class Availability
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Doktori ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Doktori ID peab olema suurem kui 0")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Nädalapäev on kohustuslik")]
    [Range(0, 6, ErrorMessage = "Nädalapäev peab olema 0-6 vahel")]
    public int DayOfWeek { get; set; }

    [Required(ErrorMessage = "Algusaeg on kohustuslik")]
    public TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "Lõpuaeg on kohustuslik")]
    public TimeSpan EndTime { get; set; }

    [Required(ErrorMessage = "Kuupäev on kohustuslik")]
    public DateTime Date { get; set; }

    public bool IsException { get; set; }

    [Required(ErrorMessage = "Doktor on kohustuslik")]
    public Doctor Doctor { get; set; } = default!;
}