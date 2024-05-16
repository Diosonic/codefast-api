namespace Codefast.Models;

public class ControleMataMata
{
    public int Id { get; set; }
    public string StatusValidacao {  get; set; }
    public bool DisputaTerceiroLugar { get; set; }  

    public int EquipeId { get; set; } 
    public Equipe Equipe { get; set; } = null!;
}
