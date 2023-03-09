using Microsoft.EntityFrameworkCore;

namespace ShiftsLoggerAPI.Models;

public class ShiftsLoggerContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    public ShiftsLoggerContext(DbContextOptions<ShiftsLoggerContext> options)
            : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShiftsLogger;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}
