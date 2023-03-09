using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerAPI.Models;

public class Shift
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    [Required]
    public Worker? Worker { get; set; }
}
