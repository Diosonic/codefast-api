namespace Codefast.Models;

public class Torneio
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public IEnumerable<Equipe> Equipes { get; set; }
}