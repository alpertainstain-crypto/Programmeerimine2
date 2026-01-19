System.ComponentModel.DataAnnotations;

public class InvoiceLine
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    [Required]
    public string Description { get; set; } = default!;
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public Invoice Invoice { get; set; } = default!;
}