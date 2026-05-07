using System;
using System.ComponentModel.DataAnnotations;

public class ToDoList
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = default!;

    [StringLength(1000)]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DueDate { get; set; }
}
