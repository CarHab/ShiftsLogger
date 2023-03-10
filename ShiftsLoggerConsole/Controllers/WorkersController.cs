using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShiftsLoggerConsole.Models;
using System.Text;

namespace ShiftsLoggerConsole.Controllers;
public class WorkersController
{
    private string BaseUrl { get; set; }

    public WorkersController(IConfiguration configuration)
    {
        BaseUrl = configuration.GetValue<string>("BaseUrl:Development");
    }

    public async Task<List<WorkerModel>> GetWorkers()
    {
        using HttpClient client = new();
        string json = await client.GetStringAsync($"{BaseUrl}Workers");

        return JsonConvert.DeserializeObject<List<WorkerModel>>(json);
    }

    public async Task<bool> CreateWorker(string name)
    {
        string json = JsonConvert.SerializeObject(new
        {
            name
        });

        StringContent data = new(json, Encoding.UTF8, "application/json");

        using HttpClient client = new();
        HttpResponseMessage result = await client.PostAsync($"{BaseUrl}Workers", data);

        return result.IsSuccessStatusCode;
    }
}

