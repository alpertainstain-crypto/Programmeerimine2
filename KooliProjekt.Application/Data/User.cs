using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kasutaja nimi on kohustuslik")]
    [MaxLength(225, ErrorMessage = "Nimi ei saa olla pikem kui 225 märki")]
    [MinLength(1, ErrorMessage = "Nimi peab sisaldama vähemalt 1 märki")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "E-posti aadress on kohustuslik")]
    [EmailAddress(ErrorMessage = "Kehtetu e-posti aadress")]
    [MaxLength(255, ErrorMessage = "E-posti aadress ei saa olla pikem kui 255 märki")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Telefoninumber on kohustuslik")]
    [Phone(ErrorMessage = "Kehtetu telefoninumber")]
    [MaxLength(20, ErrorMessage = "Telefoninumber ei saa olla pikem kui 20 märki")]
    public string Phone { get; set; } = default!;

    [Required(ErrorMessage = "Parooliräsi on kohustuslik")]
    [MaxLength(500, ErrorMessage = "Parooliräsi ei saa olla pikem kui 500 märki")]
    public string PasswordHash { get; set; } = default!;

    [Required(ErrorMessage = "Roll on kohustuslik")]
    [MaxLength(50, ErrorMessage = "Roll ei saa olla pikem kui 50 märki")]
    public string Role { get; set; } = default!;

    [Required(ErrorMessage = "Loomise kuupäev on kohustuslik")]
    public DateTime CreatedAt { get; set; }

    public List<Appointment> Appointments { get; set; } = new();
}