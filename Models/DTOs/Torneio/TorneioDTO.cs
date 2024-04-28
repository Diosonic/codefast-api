using Codefast.Models.DTOs.Equipe;

namespace Codefast.Models.DTOs.Torneio;

public class TorneioDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public IEnumerable<EquipeDTO>? Equipes { get; set; }
}
