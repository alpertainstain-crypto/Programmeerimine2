using System.ComponentModel.DataAnnotations;

public class Service
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(1)]
    public string Code { get; set; } = default!;

    [Required]
    [MaxLength(500)]
    [MinLength(1)]
    public string Description { get; set; } = default!;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}
