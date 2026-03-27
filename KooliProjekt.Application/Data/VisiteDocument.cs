using System;
using System.ComponentModel.DataAnnotations;

public class VisitDocument
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vastuvõtu ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Vastuvõtu ID peab olema suurem kui 0")]
    public int AppointmentId { get; set; }

    [Required(ErrorMessage = "Failite tee on kohustuslik")]
    [MaxLength(500, ErrorMessage = "Failite tee ei saa olla pikem kui 500 märki")]
    [MinLength(1, ErrorMessage = "Failite tee peab sisaldama vähemalt 1 märki")]
    public string FilePath { get; set; } = default!;

    [Required(ErrorMessage = "Faili tüüp on kohustuslik")]
    [MaxLength(50, ErrorMessage = "Faili tüüp ei saa olla pikem kui 50 märki")]
    [MinLength(1, ErrorMessage = "Faili tüüp peab sisaldama vähemalt 1 märki")]
    public string FileType { get; set; } = default!;

    [Required(ErrorMessage = "Üleslaadija ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Üleslaadija ID peab olema suurem kui 0")]
    public int UploadedBy { get; set; }

    [Required(ErrorMessage = "Loomise kuupäev on kohustuslik")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Vastuvõtt on kohustuslik")]
    public Appointment Appointment { get; set; } = default!;
}