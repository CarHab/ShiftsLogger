using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShiftsLoggerConsole.Models;
using System.Globalization;
using System.Text;

namespace ShiftsLoggerConsole.Controllers;
public class ShiftsController
{
    public string BaseUrl { get; set; }
    public ShiftsController(IConfiguration configuration)
    {
        BaseUrl = configuration.GetValue<string>("BaseUrl:Development");
    }

    public async Task<List<ShiftDTO>> GetAllShifts()
    {
        using HttpClient client = new();
        string json = await client.GetStringAsync($"{BaseUrl}Shifts");

        return JsonConvert.DeserializeObject<List<ShiftDTO>>(json);
    }

    public async Task<bool> CreateShift(string start, string end, string id)
    {
        string json = JsonConvert.SerializeObject(new
        {
            start = DateTime.Parse(start, CultureInfo.CurrentCulture),
            end = DateTime.Parse(end, CultureInfo.CurrentCulture),
            workerId = Convert.ToInt32(id)
        });

        StringContent data = new (json, Encoding.UTF8, "application/json");

        using HttpClient client = new();
        HttpResponseMessage result = await client.PostAsync($"{BaseUrl}Shifts", data);

        return result.IsSuccessStatusCode;
    }
    
    public async Task<List<ShiftModel>> GetShiftsByWorker(string id)
    {
        using HttpClient client = new();
        string json = await client.GetStringAsync($"{BaseUrl}Shifts/Worker/{id}");

        return JsonConvert.DeserializeObject<List<ShiftModel>>(json);
    }
}
