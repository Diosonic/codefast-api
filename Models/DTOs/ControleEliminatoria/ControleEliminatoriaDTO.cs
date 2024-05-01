using System.Text.Json.Serialization;

namespace Codefast.Models.DTOs.ControleEliminatoria
{
    public class ControleEliminatoriaDTO
    {
        public int Id { get; set; }
        public string StatusValidacao { get; set; } = string.Empty;
        public bool IsDesclassificado { get; set; }
        public int Pontuacao { get; set; }
        public TimeSpan Tempo { get; set; }

        public int EquipeId { get; set; }
        //[JsonIgnore]
        //public Equipe Equipe { get; set; } = null!;
    }
}
