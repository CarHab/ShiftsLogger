using Microsoft.Extensions.Configuration;
using ShiftsLoggerConsole.Controllers;

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
    Console.Write(">");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await RegisterWorker();
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
}

async Task RegisterWorker()
{
    Console.Write("Name: ");
    string name = Console.ReadLine();

    bool response = await workers.CreateWorker(name);

    if (response)
    {
        await Console.Out.WriteLineAsync("Worker created sucessfully");
    } else
    {
        await Console.Out.WriteLineAsync("An error occurred");
    }
    Console.Write("Press any button to return");
    Console.ReadKey(true);
}

