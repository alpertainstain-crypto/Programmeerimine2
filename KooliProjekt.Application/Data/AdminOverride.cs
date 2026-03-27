

using System;
using System.ComponentModel.DataAnnotations;

public class AdminOverride
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Doktori ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Doktori ID peab olema suurem kui 0")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Algusaeg on kohustuslik")]
    public DateTime Start { get; set; }

    [Required(ErrorMessage = "Lõpuaeg on kohustuslik")]
    public DateTime End { get; set; }

    [Required(ErrorMessage = "Põhjus on kohustuslik")]
    [MaxLength(250, ErrorMessage = "Põhjus ei saa olla pikem kui 250 märki")]
    [MinLength(1, ErrorMessage = "Põhjus peab sisaldama vähemalt 1 märki")]
    public string Reason { get; set; } = default!;

    [Required(ErrorMessage = "Looja ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Looja ID peab olema suurem kui 0")]
    public int CreatedBy { get; set; }

    [Required(ErrorMessage = "Doktor on kohustuslik")]
    public Doctor Doctor { get; set; } = default!;
}
