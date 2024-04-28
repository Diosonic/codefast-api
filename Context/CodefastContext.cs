using Codefast.Models;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Context;

public class CodefastContext : DbContext
{

    public CodefastContext(DbContextOptions<CodefastContext> options) : base(options)
    {
    }

    public DbSet<Equipe> Equipes {  get; set; }
    public DbSet<Torneio> Torneios { get; set; }
    public DbSet<ControleEliminatoria> ControleEliminatorias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
    }

}
