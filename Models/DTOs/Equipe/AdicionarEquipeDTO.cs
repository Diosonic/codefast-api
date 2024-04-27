namespace Codefast.Models.DTOs.Equipe;

public class AdicionarEquipeDTO
{
    public string Nome { get; set; }
    public string NomeParticipantes { get; set; } // improvisado apenas para termos o controle depois de termos eliminado a "Entidade Participantes"
    public int TorneioId { get; set; }
}
