using System;
using System.ComponentModel.DataAnnotations;

public class AdminOverride
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [Required]
    [MaxLength(250)]
    [MinLength(1)]
    public string Reason { get; set; } = default!;
    [Required]
    [MaxLength(60)]
    [MinLength(10)]
    public int CreatedBy { get; set; }
    [Required]
    [MaxLength(60)]
    [MinLength(1)]
    public Doctor Doctor { get; set; } = default!;
}
