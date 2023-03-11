namespace ShiftsLoggerAPI.Models;

public class ShiftDTO
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Name { get; set; }
}
