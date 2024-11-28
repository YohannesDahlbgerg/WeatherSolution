using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherProject.Core;

namespace Väderdata.Core;

public class WeatherContext : DbContext
{
    private const string connectionString =
        "Server=(localdb)\\MSSQLLocalDB;Database=Väderdata;Trusted_Connection=True;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Väder> VäderData { get; set; }
}
