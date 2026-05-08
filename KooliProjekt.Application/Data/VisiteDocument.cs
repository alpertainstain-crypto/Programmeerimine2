using System;
using System.ComponentModel.DataAnnotations;

public class VisitDocument
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int AppointmentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string FileType { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int UploadedBy { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public Appointment Appointment { get; set; } = default!;
}