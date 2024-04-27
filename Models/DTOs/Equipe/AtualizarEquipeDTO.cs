namespace Codefast.Models.DTOs.Equipe
{
    public class AtualizarEquipeDTO
    {
        public string Nome { get; set; } 
        public bool IsCredenciado { get; set; }
        public bool IsDesclassificado { get; set; }
        public int Pontuacao { get; set; }
        public string StatusValidacao { get; set; } 
        public string NomeParticipantes { get; set; } // improvisado apenas para termos o controle depois de termos eliminado a "Entidade Participantes"
    }
}
