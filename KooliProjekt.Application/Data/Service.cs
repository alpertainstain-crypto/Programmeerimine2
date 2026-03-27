using System.ComponentModel.DataAnnotations;

public class Service
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Teenuse kood on kohustuslik")]
    [MaxLength(20, ErrorMessage = "Kood ei saa olla pikem kui 20 märki")]
    [MinLength(1, ErrorMessage = "Kood peab sisaldama vähemalt 1 märki")]
    public string Code { get; set; } = default!;

    [Required(ErrorMessage = "Teenuse kirjeldus on kohustuslik")]
    [MaxLength(500, ErrorMessage = "Kirjeldus ei saa olla pikem kui 500 märki")]
    [MinLength(1, ErrorMessage = "Kirjeldus peab sisaldama vähemalt 1 märki")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Ühikuhind on kohustuslik")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Ühikuhind peab olema suurem kui 0")]
    public decimal UnitPrice { get; set; }
}
