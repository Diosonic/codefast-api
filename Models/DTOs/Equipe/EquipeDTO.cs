namespace Codefast.Models.DTOs.Equipe;

public class EquipeDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public bool IsCredenciado { get; set; }
    public bool IsDesclassificado { get; set; }
    public int Pontuacao { get; set; }
}
