System.CoponentModel.DataAnnotations;

public class AdminOverride
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    [Required]
    public string Reason { get; set; } = default!;
    [Required]
    public int CreatedBy { get; set; }
    [Required]
    public Doctor Doctor { get; set; } = default!;
}
