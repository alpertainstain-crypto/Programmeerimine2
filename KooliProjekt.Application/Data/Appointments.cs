using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public DateTime Time { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    [Required]
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = default!;
}
