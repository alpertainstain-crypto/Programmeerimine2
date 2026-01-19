System.ComponentModel.DataAnnotations;

public class Doctor
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Specialty { get; set; } = default!;

    public List<Appointment> Appointments { get; set; } = new();
}