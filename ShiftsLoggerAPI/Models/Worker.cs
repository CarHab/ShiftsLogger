using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerAPI.Models;

public class Worker
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}
