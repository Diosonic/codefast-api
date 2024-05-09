using System.Text.Json.Serialization;

namespace Codefast.Models;

public class SementeRodada
{
    public int Id { get; set; }
    public int RodadaId { get; set; }
    [JsonIgnore]
    public Rodada Rodada { get; set; }

    public virtual IEnumerable<Equipe> Equipes { get; set; }
}
