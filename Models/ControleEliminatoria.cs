using System.Text.Json.Serialization;

namespace Codefast.Models
{
    public class ControleEliminatoria
    {
        public int Id { get; set; }
        public string StatusValidacao { get; set; }
        public int Pontuacao { get; set; }
        public TimeSpan Tempo { get; set; }
        public int EquipeId { get; set; }
        [JsonIgnore]
        public Equipe Equipe { get; set; } = null!;
        public bool IsDesclassificado { get; set; }
    }
}

