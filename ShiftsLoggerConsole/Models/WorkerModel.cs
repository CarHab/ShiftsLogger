using System.Text.Json.Serialization;

namespace ShiftsLoggerConsole.Models;
public class WorkerModel
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }
}
