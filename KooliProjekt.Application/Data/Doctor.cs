using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = default!;
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = default!;
    [Required]
    [MaxLength(100)]

    public string Specialty { get; set; } = default!;

    public List<Availability> Availabilities { get; set; } = new List<Availability>();
}