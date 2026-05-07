using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public Doctor Doctor { get; set; } = default!;
}