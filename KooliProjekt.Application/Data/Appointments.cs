using System;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    public Doctor Doctor { get; set; } = default!;
}
