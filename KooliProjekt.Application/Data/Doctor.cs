System.ComponentModel.DataAnnotations;

public class Doctor
{
    public int Id { get; set; }
    [Required]
    [MaxLength(225)]
    [MinLength(1)]
    public string Name { get; set; } = default!;
    [Required]
    [MaxLength(225)]
    [MinLength(1)]
    public string Specialty { get; set; } = default!;

    public List<Appointment> Appointments { get; set; } = new();
}