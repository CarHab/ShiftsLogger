namespace ShiftsLoggerConsole.Models;
public class ShiftModel
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int WorkerId { get; set; }
}
