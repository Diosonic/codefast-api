using System.Text.Json.Serialization;

namespace Codefast.Models;

public class Equipe
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool IsCredenciado { get; set; }
    public bool IsDesclassificado { get; set; }
    public int Pontuacao { get; set; }
    public string StatusValidacao { get; set; } = string.Empty;
    public string NomeParticipantes { get; set; } = string.Empty; // improvisado apenas para termos o controle depois de termos eliminado a "Entidade Participantes"
    public int? TorneioId { get; set; }

    [JsonIgnore]
    public Torneio? Torneio { get; set; }  
}