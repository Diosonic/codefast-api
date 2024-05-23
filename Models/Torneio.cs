namespace Codefast.Models;

public class Torneio
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public IEnumerable<Equipe> Equipes { get; set; }
}