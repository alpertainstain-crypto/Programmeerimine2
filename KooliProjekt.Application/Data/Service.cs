System.ComponentModel.DataAnnotations;

public class Service
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    [Required]
    public string Description { get; set; } = default!;
    [Required]
    public decimal UnitPrice { get; set; }
}
