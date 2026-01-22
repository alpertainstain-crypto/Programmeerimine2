System.ComponentModel.DataAnnotations;

public class Availability
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public int DayOfWeek { get; set; }
    [Required]
    public TimeSpan StartTime { get; set; }
    [Required]
    public TimeSpan EndTime { get; set; }
    [Required]
    public Date Date { get; set; }

    public bool IsException { get; set; }
    [Required]
    [MaxLength(225)]
    [MinLength(1)]
    public Doctor Doctor { get; set; } = default!;
}