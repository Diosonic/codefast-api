namespace Codefast.Models.DTOs.Equipe;

public class AdicionarEquipeDTO
{
    public required string Nome { get; set; }
    public required string NomeParticipantes { get; set; } 
    public int TorneioId { get; set; }
}
