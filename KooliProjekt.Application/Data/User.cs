using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Phone { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    [Required]
    public string Role { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public List<Appointment> Appointments { get; set; } = new();
}