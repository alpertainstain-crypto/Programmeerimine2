using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = default!;

    [Required]
    public DateTime CreatedAt { get; set; }

    public List<Appointment> Appointments { get; set; } = new();
}