using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShiftsLoggerConsole.Controllers;
using System.Runtime.InteropServices;

ConfigurationBuilder builder = new();

builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

IConfiguration configuration = builder.Build();
WorkersController workers = new(configuration);
ShiftsController shifts = new(configuration);

bool IsRunning = true;

while (IsRunning)
{
    ShowMenu();
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await RegisterWorker();
            break;
        case "2":
            await ListWorkers();
            break;
        case "3":
            await CreateShift();
            break;
        case "4":
            await ListAllShifts();
            break;
        case "5":
            await ListShiftsByWorker();
            break;
    }
}

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("------ Main Menu ------");
    Console.WriteLine("1 - Register a worker");
    Console.WriteLine("2 - Search or list workers");
    Console.WriteLine("3 - Log a shift");
    Console.WriteLine("4 - List all shifts");
    Console.WriteLine("5 - List shifts by worker");
    Console.Write(">");
}

async Task RegisterWorker()
{
    Console.Write("Name: ");
    string name = Console.ReadLine();

    bool response = await workers.CreateWorker(name);

    if (response)
    {
        await Console.Out.WriteLineAsync("Worker created sucessfully");
    }
    else
    {
        await Console.Out.WriteLineAsync("An error occurred");
    }
    Console.Write("Press any key to return");
    Console.ReadKey(true);
}

async Task ListWorkers()
{
    Console.WriteLine("Type an Id to search, leave empty to list all");
    Console.Write(">");
    string input = Console.ReadLine();

    if (input.IsNullOrEmpty())
    {
        var result = await workers.GetWorkers();
        foreach (var item in result)
        {
            Console.WriteLine($"{item.Id} - {item.Name}");
        }
    }
    else if (!input.All(Char.IsNumber))
    {
        Console.WriteLine("Invalid id");
    }
    else
    {
        var result = await workers.GetOneWorker(input);
        Console.WriteLine($"{result.Id} - {result.Name}");
    }
    Console.Write("Press any key to return");
    Console.ReadKey(true);
}

async Task CreateShift()
{
    Console.Write("Start time(dd/mm/yy hh:mm): ");
    string startString = Console.ReadLine();

    Console.Write("End time(dd/mm/yy hh:mm): ");
    string endString = Console.ReadLine();

    Console.Write("Id of the worker: ");
    string idString = Console.ReadLine();

    bool result = await shifts.CreateShift(startString, endString, idString);

    if (result)
    {
        Console.WriteLine("Shift logged successfully");
    }
    else
    {
        Console.WriteLine("An error occurred");
    }
    Console.WriteLine("Press any key to return");
    Console.ReadKey(true);
}

async Task ListAllShifts()
{
    var result = await shifts.GetAllShifts();
    foreach (var item in result)
    {
        Console.WriteLine($"{item.Id} / {item.Start} -> {item.End} / {item.Name}");
    }
    Console.Write("Press any key to return");
    Console.ReadKey(true);
}

async Task ListShiftsByWorker()
{
    Console.Write("Id of the worker: ");
    string input = Console.ReadLine();

    var result = await shifts.GetShiftsByWorker(input);
    var worker = await workers.GetOneWorker(input);

    Console.WriteLine($"Showing results for {worker.Name}");

    foreach (var item in result)
    {
        Console.WriteLine($"{item.Id} / {item.Start} -> {item.End}");
    }
    Console.Write("Press any key to return");
    Console.ReadKey(true);
}