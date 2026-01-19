System.CoponentModel.DataAnnotations;

public class VisitDocument
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    [Required]
    public string FilePath { get; set; } = default!;
    [Required]
    public string FileType { get; set; } = default!;
    [Required]
    public int UploadedBy { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public Appointment Appointment { get; set; } = default!;
}