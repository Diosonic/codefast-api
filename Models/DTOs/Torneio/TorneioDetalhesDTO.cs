using Codefast.Models.DTOs.Equipe;

namespace Codefast.Models.DTOs.Torneio;

public class TorneioDetalhesDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public IEnumerable<EquipeDTO> Equipes { get; set; }

}
