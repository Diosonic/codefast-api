namespace Codefast.Models.DTOs.ControleMataMata;

public class ControleMataMataDTO
{
    public int Id { get; set; }
    public string StatusValidacao { get; set; }
    public bool DisputaTerceiroLugar { get; set; }

    public ControleMataMataEquipeDTO Equipe { get; set; } 
}

