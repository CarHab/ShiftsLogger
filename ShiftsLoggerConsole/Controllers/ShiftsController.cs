using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShiftsLoggerConsole.Models;
using System.Text;

namespace ShiftsLoggerConsole.Controllers;
public class ShiftsController
{
    public string BaseUrl { get; set; }
    public ShiftsController(IConfiguration configuration)
    {
        BaseUrl = configuration.GetValue<string>("BaseUrl:Development");
    }

    public async Task<HttpResponseMessage> CreateShift(ShiftModel shift)
    {
        var json = JsonConvert.SerializeObject(shift);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        using HttpClient client = new();
        var result = await client.PostAsync($"{BaseUrl}Shifts", data);

        return result;
    }
}
