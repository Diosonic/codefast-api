using System.Text.Json.Serialization;

namespace Codefast.Models;

public class Rodada
{
    public int Id { get; set; }
    public string Titulo { get; set; }

    public int TorneioId { get; set; }
    [JsonIgnore]
    public Torneio? Torneio { get; set; }
    public ICollection<SementeRodada> SementeRodadas { get; set; }    
}
