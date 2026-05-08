using System.ComponentModel.DataAnnotations;

public class InvoiceLine
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int InvoiceId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = default!;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    public Invoice Invoice { get; set; } = default!;
}