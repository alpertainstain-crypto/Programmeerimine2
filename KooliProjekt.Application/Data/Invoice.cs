System.CoponentModel.DataAnnotations;

public class Invoice
{
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public string InvoiceNo { get; set; } = default!;
    [Required]
    public DateTime InvoiceDate { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public string Status { get; set; } = default!;
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal GrandTotal { get; set; }
    public DateTime? MarkedPaidAt { get; set; }
    [Required]
    [MinLength(1)]
    public Appointment Appointment { get; set; } = default!;
    public int AppointmentId { get; set; }

    public List<InvoiceLine> Lines { get; set; } = new();
}
