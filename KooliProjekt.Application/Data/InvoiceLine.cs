using System.ComponentModel.DataAnnotations;
using KooliProjekt.Application.Data;

public class InvoiceLine : Entity
{

    [Required(ErrorMessage = "Arve ID on kohustuslik")]
    [Range(1, int.MaxValue, ErrorMessage = "Arve ID peab olema suurem kui 0")]
    public int InvoiceId { get; set; }

    [Required(ErrorMessage = "Kirjeldus on kohustuslik")]
    [MaxLength(500, ErrorMessage = "Kirjeldus ei saa olla pikem kui 500 märki")]
    [MinLength(1, ErrorMessage = "Kirjeldus peab sisaldama vähemalt 1 märki")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Summa on kohustuslik")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Summa peab olema suurem kui 0")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Arve on kohustuslik")]
    public Invoice Invoice { get; set; } = default!;
}