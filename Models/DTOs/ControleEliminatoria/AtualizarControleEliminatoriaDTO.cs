namespace Codefast.Models.DTOs.ControleEliminatoria;

public class AtualizarControleEliminatoriaDTO
{
    public string StatusValidacao { get; set; }
    public int Pontuacao { get; set; }
    public TimeSpan Tempo { get; set; }
}
