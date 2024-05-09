using Codefast.Models.DTOs.Equipe;
using System.Text.Json.Serialization;

namespace Codefast.Models.DTOs.ControleEliminatoria
{
    public class ControleEliminatoriaDTO
    {
        public int Id { get; set; }
        public string StatusValidacao { get; set; }
        public bool IsDesclassificado { get; set; }
        public int Pontuacao { get; set; }
        public TimeSpan Tempo { get; set; }

        public EquipeDTO Equipe { get; set; }

    }
}
