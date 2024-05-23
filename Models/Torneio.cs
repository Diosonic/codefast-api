namespace Codefast.Models;

public class Torneio
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool isTempoCorrendo { get; set; } = false;
    public bool isNovaRodada { get; set; } = true;
    public TimeSpan Tempo { get; set; }
    public IEnumerable<Equipe> Equipes { get; set; }
}