using System;
using System.ComponentModel.DataAnnotations;

public class AdminOverride
{
    [Key]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [Required]
    [MaxLength(100)]
    public string Reason { get; set; }
    public string IsAdmin { get; set; }
    public int CreatedBy { get; set; }
    [Required]
    public Doctor Doctor { get; set; } 
}
