namespace Codefast.Models.DTOs.Equipe;

public class EquipeDTO
{
    public int Id { get; set; }
    public string? TituloTorneio { get; set; }
    public string Nome { get; set; }
    public string NomeParticipantes { get; set; } 
    public bool IsCredenciado { get; set; }
}
